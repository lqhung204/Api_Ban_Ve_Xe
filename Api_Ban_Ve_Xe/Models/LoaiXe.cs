using System;
using System.Collections.Generic;

namespace Api_Ban_Ve_Xe.Models
{
    public partial class LoaiXe
    {
        public LoaiXe()
        {
            Xes = new HashSet<Xe>();
        }

        public int MaLoaiXe { get; set; }
        public string? TenLoaiXe { get; set; }

        public virtual ICollection<Xe> Xes { get; set; }
    }
}
