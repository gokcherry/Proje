namespace WebProgramlamaProje.Models
{
    public class Musteriler
    {
        public int musteri_ID {  get; set; }
        public string ad {  get; set; }
        public string soyad { get; set; }
        public string telefon { get; set; }
        public string email { get; set; }
        public string sifre { get; set; }
        public string rol {  get; set; }
        public DateTime kayit_tarih { get; set; }
        
        public ICollection<Randevular> randevular { get; set; }
    }
}
