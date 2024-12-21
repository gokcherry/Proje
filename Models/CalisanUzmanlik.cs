using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proje.Models
{
    public class CalisanUzmanlik
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey("Calisan")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir çalışan ID giriniz.")]
        public int CalisanID { get; set; }

        [Required]
        [ForeignKey("Uzmanlik")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir uzmanlık ID giriniz.")]
        public int UzmanlikID { get; set; }

        public virtual Calisan Calisan { get; set; }
        public virtual UzmanlikAlanlari Uzmanlik { get; set; }
    }
}
