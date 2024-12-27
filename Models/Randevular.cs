using System;
using System.ComponentModel.DataAnnotations;    
using System.ComponentModel.DataAnnotations.Schema;

namespace Proje.Models
{
    public class Randevu        
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey("Musteri")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir müşteri ID giriniz.")]
        public string MusteriID { get; set; }

        [Required]
        [ForeignKey("Calisan")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir çalışan ID giriniz.")]
        public int CalisanID { get; set; }

        [Required]
        [ForeignKey("Uzmanlik")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir uzmanlık ID giriniz.")]
        public int UzmanlikID { get; set; }

        [Required(ErrorMessage = "Randevu tarihi zorunludur.")]
        public DateTime RandevuTarihi { get; set; }

        [Required(ErrorMessage = "Toplam fiyat alanı zorunludur.")]
        [Range(0, double.MaxValue, ErrorMessage = "Toplam fiyat negatif olamaz.")]
        public decimal ToplamFiyat { get; set; }

        [Required(ErrorMessage = "Durum alanı zorunludur.")]
        [StringLength(20, ErrorMessage = "Durum en fazla 20 karakter olabilir.")]
        public string Durum { get; set; }
        public Kullanicilar Musteri { get; set; }
        public virtual Calisanlar Calisan { get; set; }
        public virtual UzmanlikAlanlari Uzmanlik { get; set; }  
    }
}
