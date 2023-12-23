using System;
using System.Collections.Generic;

namespace Api_Ban_Ve_Xe.Models
{
    public partial class TaiXe
    {
        public TaiXe()
        {
            ChuyenXes = new HashSet<ChuyenXe>();
        }

        public int MaTaiXe { get; set; }
        public string? TenTaiXe { get; set; }
        public string? DiaChi { get; set; }
        public string? GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? Cccd { get; set; }
        public string? DienThoai { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<ChuyenXe> ChuyenXes { get; set; }
    }
}
