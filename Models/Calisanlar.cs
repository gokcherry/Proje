using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proje.Models
{
    public class Calisan
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir.")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Email alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string Telefon { get; set; }

        public virtual ICollection<CalisanUzmanlik> UzmanlikAlanlari { get; set; } = new List<CalisanUzmanlik>();
        public virtual ICollection<Randevu> Randevular { get; set; } = new List<Randevu>();
    }
}
