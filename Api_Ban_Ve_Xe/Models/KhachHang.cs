using System;
using System.Collections.Generic;

namespace Api_Ban_Ve_Xe.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            VeXes = new HashSet<VeXe>();
        }

        public int MaKhachHang { get; set; }
        public string? TenKhachHang { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? GioiTinh { get; set; }
        public string? DiaChi { get; set; }
        public string? Cccd { get; set; }
        public string? DienThoai { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<VeXe> VeXes { get; set; }
    }
}
