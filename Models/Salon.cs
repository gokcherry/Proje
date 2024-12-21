using System;
using System.ComponentModel.DataAnnotations;

namespace Proje.Models
{
    public class Salon
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Salon adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Salon adı en fazla 100 karakter olabilir.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Gün bilgisi zorunludur.")]
        [StringLength(20, ErrorMessage = "Gün bilgisi en fazla 20 karakter olabilir.")]
        public string Gun { get; set; }

        [Required(ErrorMessage = "Açılış saati zorunludur.")]
        public TimeSpan AcilisSaati { get; set; }

        [Required(ErrorMessage = "Kapanış saati zorunludur.")]
        public TimeSpan KapanisSaati { get; set; }
    }
}
