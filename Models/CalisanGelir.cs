using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proje.Models
{
    public class CalisanGelir
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey("Calisan")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir çalışan ID giriniz.")]
        public int CalisanID { get; set; }

        [Required(ErrorMessage = "Tarih alanı zorunludur.")]
        public DateTime Tarih { get; set; }

        [Required(ErrorMessage = "Toplam gelir alanı zorunludur.")]
        [Range(0, double.MaxValue, ErrorMessage = "Toplam gelir negatif olamaz.")]
        public decimal ToplamGelir { get; set; }
        public virtual Calisanlar Calisan { get; set; }
    }
}
