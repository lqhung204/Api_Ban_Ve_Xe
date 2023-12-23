using System;
using System.Collections.Generic;

namespace Api_Ban_Ve_Xe.Models
{
    public partial class ChiTietVeXe
    {
        public int MaVe { get; set; }
        public int SoLuongVe { get; set; }
        public DateTime NgayDat { get; set; }

        public virtual VeXe MaVeNavigation { get; set; } = null!;
        
    }
}
