namespace Api_Ban_Ve_Xe.ModelsView
{
    public class ChuyenXeViewModel
    {
        public int MaChuyenXe { get; set; }
        public string TenChuyenXe { get; set; }
        public TimeSpan? GioDi { get; set; }
        public TimeSpan? GioDen { get; set; }
        public int? ChoTrong { get; set; }
        public string DiemDi { get; set; }
        public string DiemDen { get; set; }
        public double? BangGia { get; set; }

        public string NgayDi { get; set; }
    }
}
