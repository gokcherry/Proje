using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Proje.Models
{
    public class Kullanicilar : IdentityUser
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Telefon { get; set; }
        public virtual ICollection<Randevu> Randevular { get; set; } = new List<Randevu>();
    }
}
