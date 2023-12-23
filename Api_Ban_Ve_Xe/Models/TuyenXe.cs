using System;
using System.Collections.Generic;

namespace Api_Ban_Ve_Xe.Models
{
    public partial class TuyenXe
    {
        public TuyenXe()
        {
            ChuyenXes = new HashSet<ChuyenXe>();
        }

        public int MaTuyen { get; set; }
        public string? TenTuyen { get; set; }
        public string? DiemDi { get; set; }
        public string? DiemDen { get; set; }
        public double? BangGia { get; set; }

        public virtual ICollection<ChuyenXe> ChuyenXes { get; set; }
    }
}
