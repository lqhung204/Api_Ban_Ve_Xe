using Api_Ban_Ve_Xe.ModelsView;

namespace Api_Ban_Ve_Xe.Models
{
    internal class PaymentModel
    {
        public int idKhachHang { get; set; }
        public int idChuyenXe { get; set; }
        public int idXe { get; set; }
        public int idTaiXe { get; set; }

        public DateTime NgayDat { get; set; } // Thêm thuộc tính NgayDat
        public int SoLuongVe { get; set; } // Thêm thuộc tính SoLuongVe

    }
}