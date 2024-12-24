using Microsoft.AspNetCore.Identity;

namespace Proje.Models
{
    public class Kullanicilar : IdentityUser
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
    }
}
