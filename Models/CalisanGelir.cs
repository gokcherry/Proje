﻿namespace Proje.Models
{
    public class CalisanGelir
    {
        public int ID { get; set; }
        public int calisan_ID { get; set; }
        public DateTime tarih {  get; set; }
        public decimal toplam_gelir {  get; set; }

        public Calisanlar calisan { get; set; }
    }
}
