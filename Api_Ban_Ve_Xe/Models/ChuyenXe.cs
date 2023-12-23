using System;
using System.Collections.Generic;

namespace Api_Ban_Ve_Xe.Models
{
    public partial class ChuyenXe
    {
        public ChuyenXe()
        {
            VeXes = new HashSet<VeXe>();
        }

        public int MaChuyenXe { get; set; }
        public string? TenChuyenXe { get; set; }
        public int MaTuyen { get; set; }
        public TimeSpan? GioDi { get; set; }
        public TimeSpan? GioDen { get; set; }
        public int? ChoTrong { get; set; }
        public int MaTaiXe { get; set; }
        public DateTime NgayDi { get; set; }

        public int MaXe { get; set; }

        public virtual TaiXe MaTaiXeNavigation { get; set; } = null!;
        public virtual TuyenXe MaTuyenNavigation { get; set; } = null!;
        public virtual ICollection<VeXe> VeXes { get; set; }
        public virtual Xe MaXeNavigation { get; set; } = null!;

        public string GetNgayDi()
        {
            return NgayDi.ToString("dd-MM-yyyy");
        }
    }
}
