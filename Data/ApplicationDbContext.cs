using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proje.Models;

namespace Proje.Data
{
    public class ApplicationDbContext : IdentityDbContext <Kullanicilar>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Calisanlar> Calisanlar { get; set; }
        public DbSet<CalisanGelir> CalisanGelir { get; set; }
        public DbSet<CalisanUzmanlik> CalisanUzmanlik { get; set; }
        public DbSet<Kullanicilar> Musteri { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<Salon> Salon { get; set; }
        public DbSet<UzmanlikAlanlari> UzmanlikAlanlari { get; set; }public DbSet<Calisanlar> Calisan { get; set; }

    }
}
