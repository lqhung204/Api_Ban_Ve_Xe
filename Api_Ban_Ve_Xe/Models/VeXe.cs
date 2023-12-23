using System;
using System.Collections.Generic;

namespace Api_Ban_Ve_Xe.Models
{
    public partial class VeXe
    {
        public int MaVe { get; set; }
        public string? TenVe { get; set; }
        public int MaKhachHang { get; set; }
        public int MaChuyenXe { get; set; }
        public int SoLuongVe { get; set; }
        public DateTime NgayDat { get; set; }
        public virtual ChuyenXe MaChuyenXeNavigation { get; set; } = null!;
        public virtual KhachHang MaKhachHangNavigation { get; set; } = null!;
    }
}
