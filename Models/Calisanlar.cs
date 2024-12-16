namespace WebProgramlamaProje.Models
{
    public class Calisanlar
    {
        public int calisan_ID { get; set; }
        public string calisan_ad { get; set; }
        public string calisan_soyad { get; set; }
        public string email { get; set; }
        public string telefon { get; set; }
        public DateTime katilim_tarih { get; set; }

        public ICollection<CalisanUzmanlik> UzmanlikAlanlari { get; set; }
        public ICollection<Randevular> Randevular { get; set; }

    }
}
