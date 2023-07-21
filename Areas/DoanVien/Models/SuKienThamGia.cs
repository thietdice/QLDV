namespace QLDV.Areas.DoanVien.Models
{
    public class SuKienThamGia
    {
        public int Id { get; set; }

        public string Semester { get; set; }

        public string Title { get; set; } 

        public int Score { get; set; }

        public string Image { get; set; } = string.Empty;
    }
}
