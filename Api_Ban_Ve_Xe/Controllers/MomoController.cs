using Api_Ban_Ve_Xe.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api_Ban_Ve_Xe.Models.Order;
using Api_Ban_Ve_Xe.Services;
using System.Data.Entity;
using Api_Ban_Ve_Xe.Models.Momo;
using Org.BouncyCastle.Asn1.X9;
using MediatR;
using NuGet.Protocol;
using QRCoder;
using IronBarCode;
using System;
using System.Drawing;
using System.Linq;
using System.Drawing.Imaging;
using System.IO;
using SelectPdf;

using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using System.Drawing;
namespace Api_Ban_Ve_Xe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MomoController : ControllerBase
    {
        private IMomoService _momoService;

        public MomoController(IMomoService momoService)
        {
            _momoService = momoService;
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
                var totalPrice = tuyenXe.BangGia * soLuongGhe;

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



                OrderInfoModel model = new OrderInfoModel();
                model.FullName = khachHang.TenKhachHang;
                model.Amount = (double)totalPrice;
                model.OrderInfo = maVe.ToString();
                var response = await _momoService.CreatePaymentAsync(model);

                var payUrl = response.PayUrl;


                return Ok(new { PayUrl = payUrl });

            }

            //return Ok("Booking created successfully.");



        }
        [HttpGet]
        public IActionResult PaymentCallBack()
        {
            var response = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
            int startIndex = response.OrderInfo.IndexOf("Mã Vé:");

            // Tìm vị trí kết thúc của mã vé (là vị trí ký tự tiếp theo sau dấu hai chấm)
            int endIndex = response.OrderInfo.IndexOf(".", startIndex);

            // Lấy mã vé từ chuỗi
            string maVe = response.OrderInfo.Substring(startIndex + 7, endIndex - startIndex - 7).Trim();

            using (var context = new AppDbContext())
            {
                VeXe veXe = context.VeXes.Where(x => x.MaVe == int.Parse(maVe)).First();

                KhachHang khachHang = context.KhachHangs.Where(x => x.MaKhachHang == veXe.MaKhachHang).First();

                ChuyenXe chuyenXe = context.ChuyenXes.Where(x => x.MaChuyenXe == veXe.MaChuyenXe).First();

                TuyenXe tuyenXe = context.TuyenXes.Where(x => x.MaTuyen == chuyenXe.MaTuyen).First();

                string thongTinTenVe = "Vé Xe Đi : " + tuyenXe.DiemDi + "-" + tuyenXe.DiemDen;
                var totalPrice = chuyenXe.MaTuyenNavigation.BangGia * veXe.SoLuongVe;

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


                string fileName = maVe.ToString();
                string serverPath = $"Images/{fileName}.png";


                
                QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(maVe, QRCodeGenerator.ECCLevel.Q);
                QRCode qRCode = new QRCode(qRCodeData);
                Bitmap qrCodeBitmap = qRCode.GetGraphic(20);

                using (FileStream fs = new FileStream(serverPath, FileMode.Create))
                {
                    qrCodeBitmap.Save(fs, ImageFormat.Png);
                }

                byte[] imageBytes = System.IO.File.ReadAllBytes($"Images/{fileName}.png");
                string imageBase64 = Convert.ToBase64String(imageBytes);

                //   "http://localhost:7196/" +

                string imageLink =  $"Images/{fileName}.png";

                //string html = document.DocumentNode.OuterHtml; // Lấy nội dung HTML từ tệp tin
                string html = System.IO.File.ReadAllText(absoluteFilePath);
                html = html.Replace("{{MaVe}}", veXe.MaVe.ToString())
                    .Replace("{{NgayDat}}", ngayDatFormatted)
                    .Replace("{{ThongTinTenVe}}", thongTinTenVe)
                    .Replace("{{SoLuongGhe}}", veXe.SoLuongVe.ToString())
                    .Replace("{{GiaTien}}", giaTienFormatted)
                    .Replace("{{TotalPrice}}", totalPFormatted)
                    .Replace("{{TenKhachHang}}", khachHang.TenKhachHang)
            .Replace("{{DiaChi}}", khachHang.DiaChi)
            .Replace("{{EmailKhachHang}}", khachHang.Email)
            .Replace("{{SoDienThoai}}", khachHang.DienThoai)
            .Replace("{{QR_CODE_PLACEHOLDER}}", "data:image/png;base64," + imageBase64)
            ;


                html = html.Replace("StrTag", "<").Replace("EndTag", ">");
                HtmlToPdf htmlToPdf = new HtmlToPdf();
                PdfDocument pdfDocument = htmlToPdf.ConvertHtmlString(html);
                byte[] pdf = pdfDocument.Save();
                string folderPath = "Ticket";
                string fileNamePdf = maVe.ToString() + ".pdf";
                string filePathPdf = Path.Combine(folderPath, fileNamePdf);

                Directory.CreateDirectory(folderPath); // Tạo thư mục nếu chưa tồn tại
                System.IO.File.WriteAllBytes(filePathPdf, pdf);
                
                pdfDocument.Close();


                string Content = @"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {
            font-family: Arial, sans-serif;
            line-height: 1.5;
        }

        h1 {
            font-size: 24px;
            margin-bottom: 20px;
        }

        p {
            font-size: 16px;
            margin-bottom: 10px;
        }

        .ticket-details {
            margin-top: 30px;
            border: 1px solid #ccc;
            padding: 10px;
        }

        .contact-info {
            margin-top: 20px;
        }
    </style>
</head>
<body>
    <h1>Cảm ơn quý khách đã đặt vé!</h1>
    
    <p>Xin chào [Tên khách hàng],</p>
    
    <p>Chúng tôi gửi email này để xác nhận rằng quý khách đã thành công đặt vé với chúng tôi. Chúng tôi rất vui mừng được phục vụ quý khách và chúng tôi tận hưởng việc mang đến cho quý khách trải nghiệm đi xe tuyệt vời.</p>
    
    <div class=""ticket-details"">
        <h2>Thông tin vé:</h2>
        <p><strong>Tên khách hàng:</strong> [Tên khách hàng]</p>
        <p><strong>Số điện thoại:</strong> [Số điện thoại]</p>
        <p><strong>Email:</strong> [Email]</p>
        <p><strong>Ngày đặt vé:</strong> [Ngày đặt vé]</p>
<p><strong>Số lượng Vé:</strong> [So Luong vé] Vé</p>
<p><strong>Giá Tiền:</strong> [Gia Ve]/Ghế</p>
        <p><strong>Tổng cộng:</strong> [Tổng cộng]</p>
    </div>

    <p>Nếu quý khách có bất kỳ câu hỏi hoặc thắc mắc nào, xin vui lòng liên hệ với chúng tôi qua thông tin dưới đây:</p>

    <div class=""contact-info"">
        <p><strong>Địa chỉ:</strong> 2829 Huỳnh Tấn Phát, Nhà Bè, Hồ Chí Minh</p>
        <p><strong>Số điện thoại:</strong> 0703201087</p>
        <p><strong>Email:</strong> datvere@gmail.com</p>
    </div>

    <p>Một lần nữa, chúng tôi xin chân thành cảm ơn quý khách đã lựa chọn dịch vụ của chúng tôi. Chúng tôi rất mong được đón tiếp quý khách trong hành trình sắp tới.</p>

    <p>Trân trọng,</p>
    <p>Dat Ve Re Company</p>
</body>
</html>
";

              
                Content = Content
                    .Replace("[Ngày đặt vé]", ngayDatFormatted)
                    .Replace("[Thông Tin Tên Vé]", thongTinTenVe)
                    .Replace("[So Luong vé]", veXe.SoLuongVe.ToString())
                    .Replace("[Gia Ve]", giaTienFormatted)
                    .Replace("[Tổng cộng]", totalPFormatted)
                    .Replace("[Tên khách hàng]", khachHang.TenKhachHang)
            .Replace("[Email]", khachHang.Email)
            .Replace("[Số điện thoại]", khachHang.DienThoai)
            
            ;
                // string c = "<img src=\"{{QR_CODE_PLACEHOLDER}}\" alt=\"QR Code\">";
                //html = html.Replace(c, " ");
                var result = Api_Ban_Ve_Xe.Common.Common.SendMail("Đặt Vé Xe", "Vé #" + veXe.MaVe.ToString(), Content, khachHang.Email, filePathPdf);
                if (result == false)
                {
                    return BadRequest(result);
                }

            }

            return Ok("Booking created successfully.");
            Console.WriteLine(maVe);
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
                bool exists;
                using (var context = new AppDbContext())
                {
                    // Kiểm tra xem mã vé đã tồn tại trong cơ sở dữ liệu chưa
                     exists = context.VeXes.Any(v => v.MaVe == maVe);
                }
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
