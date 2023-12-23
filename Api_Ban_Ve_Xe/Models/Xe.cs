using System;
using System.Collections.Generic;

namespace Api_Ban_Ve_Xe.Models
{
    public partial class Xe
    {
        public int MaXe { get; set; }
        public string? TenXe { get; set; }
        public string? BienSo { get; set; }
        public string? SoGhe { get; set; }
        public int MaLoaiXe { get; set; }

        public virtual LoaiXe MaLoaiXeNavigation { get; set; } = null!;
    }
}
