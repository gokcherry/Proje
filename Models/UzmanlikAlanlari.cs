namespace Proje.Models
{
    public class UzmanlikAlanlari
    {
        public int ID { get; set; }
        public required string ad {  get; set; }
        public int sure { get; set; }
        public decimal fiyat { get; set; }

        public  ICollection<CalisanUzmanlik> calisanlar { get; set; }

    }
}

