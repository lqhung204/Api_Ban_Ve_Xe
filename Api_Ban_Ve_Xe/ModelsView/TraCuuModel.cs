using Api_Ban_Ve_Xe.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Ban_Ve_Xe.ModelsView
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraCuuModel : ControllerBase
    {
        public int  idKhachHang { get; set; }
        public int idChuyenXe { get; set; }
        public int idXe { get; set; }
        public int idTaiXe { get; set; }

        public DateTime NgayDat { get; set; } // Thêm thuộc tính NgayDat
        public int SoLuongVe { get; set; } // Thêm thuộc tính SoLuongVe
    }
}
