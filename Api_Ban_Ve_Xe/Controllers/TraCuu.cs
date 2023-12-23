using Api_Ban_Ve_Xe.Models;
using Api_Ban_Ve_Xe.ModelsView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Api_Ban_Ve_Xe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraCuu : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public TraCuu(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{maVe}")]
        public  IActionResult GetVeDetails(int maVe)
        {

            
            PaymentModel modelView = new PaymentModel();

            using (var dbContext = new AppDbContext())
            {
                // Tìm vé xe với mã vé
                VeXe ve = dbContext.VeXes.FirstOrDefault(x => x.MaVe == maVe);
                if (ve == null)
                {
                    return NotFound("No Ve"); // Nếu không tìm thấy vé, trả về lỗi 404
                }

                // Lấy thông tin khách hàng
                KhachHang khachHang = dbContext.KhachHangs.Where(x => x.MaKhachHang == ve.MaKhachHang).First();
                if (khachHang == null)
                {
                    return NotFound("no khac"); // Nếu không tìm thấy khách hàng, trả về lỗi 404
                }

                // Lấy thông tin chuyến xe
                 ChuyenXe chuyenXe = dbContext.ChuyenXes.Where(x=>x.MaChuyenXe == ve.MaChuyenXe).First();
                if (chuyenXe == null)
                {
                    return NotFound("no chuyen xe"); // Nếu không tìm thấy chuyến xe, trả về lỗi 404
                }

                // Lấy thông tin xe
                Xe xe = dbContext.Xes.Where(x=>x.MaXe == chuyenXe.MaXe).First();
                if (xe == null)
                {
                    return NotFound("no ma xe"); // Nếu không tìm thấy xe, trả về lỗi 404
                }

                // Lấy thông tin tài xế
                TaiXe taiXe = dbContext.TaiXes.Where(x=>x.MaTaiXe== chuyenXe.MaTaiXe).First();
                if (taiXe == null)
                {
                    return NotFound("no tai xe"); // Nếu không tìm thấy tài xế, trả về lỗi 404
                }

                //ChiTietVeXe chiTietVeXe = dbContext.ChiTietVeXes.Where(x=>x.MaVe == maVe).First();
                //if (taiXe == null)
                //{
                //    return NotFound("no chi tiet"); // Nếu không tìm thấy tài xế, trả về lỗi 404
                //}
                // Tạo đối tượng kết quả

                modelView.idXe = xe.MaXe;
                modelView.idChuyenXe = chuyenXe.MaChuyenXe;
                modelView.idKhachHang = khachHang.MaKhachHang;
                modelView.idTaiXe = taiXe.MaTaiXe;
                modelView.NgayDat = ve.NgayDat;
                modelView.SoLuongVe = ve.SoLuongVe;


            
                return Ok(modelView); // Trả về kết quả thành công
            }
        }
    }
}
