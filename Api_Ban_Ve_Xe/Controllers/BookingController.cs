using Api_Ban_Ve_Xe.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Net.Mail;
using Grpc.Core;
using HtmlAgilityPack;
using System.IO;
using Api_Ban_Ve_Xe.Services;
using Api_Ban_Ve_Xe.Models.Order;

namespace Api_Ban_Ve_Xe.Controllers
{
    [ApiController]
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;
        }

       
        [HttpPost]
        public async Task<IActionResult> CreateBookingAsync(int maChuyenXe, KhachHang khachHang, int soLuongGhe)
        {
            int maVe = GenerateUniqueMaVe();
            using (var context = new AppDbContext())
            {
                ChuyenXe chuyenXe = context.ChuyenXes.FirstOrDefault(x => x.MaChuyenXe.Equals(maChuyenXe));

                if (chuyenXe == null)
                {
                    return BadRequest("Có Lỗi Kĩ Thuật");
                }

                string tenVe = "Vé Xe " + chuyenXe.TenChuyenXe.ToString();

                TuyenXe tuyenXe = context.TuyenXes.FirstOrDefault(x => x.MaTuyen == chuyenXe.MaTuyen);


                // Lưu thông tin khách hàng vào cơ sở dữ liệu
                khachHang.MaKhachHang = int.Parse(khachHang.Cccd);
                context.KhachHangs.Add(khachHang);
                


                // Tạo vé xe
                VeXe veXe = new VeXe();
                veXe.MaVe = maVe;
                veXe.TenVe = tenVe;
                veXe.MaKhachHang = khachHang.MaKhachHang;
                veXe.MaChuyenXe = maChuyenXe;
                veXe.SoLuongVe = soLuongGhe;
                veXe.NgayDat = DateTime.Today;
                context.VeXes.Add(veXe);

                context.SaveChanges();
                //

                //var email = new MimeMessage();
                //email.From.Add(MailboxAddress.Parse("lqhung420it@gmail.com"));
                //email.To.Add(MailboxAddress.Parse(khachHang.Email.ToString()));
                //email.Subject = "Đặt Hàng Thành Công";
                //email.Body = new TextPart(TextFormat.Html) { Text = body};

                //using var smtp = new SmtpClient();
                //smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
                //smtp.Authenticate("lqhung420it@gmail.com", "hungle123");
                //smtp.Send(email);
                //smtp.Disconnect(true);
                //SendMail(khachHang.Email.ToString(), "Đặt Vé Thành Công", body);


                string thongTinTenVe = "Vé Xe Đi : " + tuyenXe.DiemDi + "-" + tuyenXe.DiemDen;
                var totalPrice = chuyenXe.MaTuyenNavigation.BangGia * soLuongGhe;

                decimal giaTien = Convert.ToDecimal(chuyenXe.MaTuyenNavigation.BangGia);
                decimal totalP = Convert.ToDecimal(totalPrice);

                string giaTienFormatted = giaTien.ToString("N0");
                string totalPFormatted = totalP.ToString("N0");

                DateTime ngayDat = veXe.NgayDat;
                string ngayDatFormatted = ngayDat.ToString("dd/MM/yyyy");

                //send mail 
                string filePath = "C:\\Users\\MT115\\source\\repos\\Api_Ban_Ve_Xe\\Api_Ban_Ve_Xe\\template\\send1.html";
                string absoluteFilePath = Path.GetFullPath(filePath);

                HtmlDocument document = new HtmlDocument();
                document.Load(absoluteFilePath);

                //string html = document.DocumentNode.OuterHtml; // Lấy nội dung HTML từ tệp tin
                string html = System.IO.File.ReadAllText(absoluteFilePath);
                html = html.Replace("{{MaVe}}", veXe.MaVe.ToString())
                    .Replace("{{NgayDat}}", ngayDatFormatted)
                    .Replace("{{ThongTinTenVe}}", thongTinTenVe)
                    .Replace("{{SoLuongGhe}}", soLuongGhe.ToString())
                    .Replace("{{GiaTien}}", giaTienFormatted)
                    .Replace("{{TotalPrice}}", totalPFormatted)
                    .Replace("{{TenKhachHang}}", khachHang.TenKhachHang)
            .Replace("{{DiaChi}}", khachHang.DiaChi)
            .Replace("{{EmailKhachHang}}", khachHang.Email)
            .Replace("{{SoDienThoai}}", khachHang.DienThoai);



             //  var result= Api_Ban_Ve_Xe.Common.Common.SendMail("Đặt Vé Xe", "Vé #" + veXe.MaVe.ToString(), html, khachHang.Email, filePathPdf);
              //  if (result == false)
              //  {
              ///      return BadRequest(result);
              //  }
               

            }
            
            return Ok("Booking created successfully.");
        }

        private int GenerateRandomMaVe()
        {
            Random random = new Random();
            int maVe = random.Next(100000, 999999); // Tạo số ngẫu nhiên từ 100000 đến 999999

            return maVe;
        }
        private int GenerateUniqueMaVe()
        {
            int maVe;
            bool isUnique = false;

            do
            {
                // Tạo mã vé ngẫu nhiên
                maVe = GenerateRandomMaVe();

                // Kiểm tra xem mã vé đã tồn tại trong cơ sở dữ liệu chưa
                bool exists = _context.VeXes.Any(v => v.MaVe == maVe);

                // Nếu mã vé chưa tồn tại, đánh dấu là mã vé duy nhất và thoát khỏi vòng lặp
                if (!exists)
                {
                    isUnique = true;
                }
            } while (!isUnique);

            return maVe;
        }

        
        }    

        
       }
    

