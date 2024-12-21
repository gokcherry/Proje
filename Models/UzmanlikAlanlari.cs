using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proje.Models
{
    public class UzmanlikAlanlari
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Uzmanlık adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Uzmanlık adı en fazla 100 karakter olabilir.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Süre zorunludur.")]
        [Range(1, int.MaxValue, ErrorMessage = "Süre en az 1 dakika olmalıdır.")]
        public int Sure { get; set; }

        [Required(ErrorMessage = "Fiyat zorunludur.")]
        [Range(0, double.MaxValue, ErrorMessage = "Fiyat negatif olamaz.")]
        public decimal Fiyat { get; set; }

        public virtual ICollection<CalisanUzmanlik> Calisanlar { get; set; } = new List<CalisanUzmanlik>();
    }
}
