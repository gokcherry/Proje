namespace Proje.Models
{
    public class Randevular
    {
        public int ID { get; set; }
        public int musteri_ID { get; set; }
        public int calisan_ID { get; set; }
        public int uzmanlik_ID { get; set; }
        public DateTime randevu_tarih {  get; set; }
        public decimal toplam_fiyat {  get; set; }
        public string durum {  get; set; }

        public Musteriler musteri { get; set; }
        public Calisanlar calisan {  get; set; }
        public UzmanlikAlanlari uzmanlik { get; set; }

    }
}
