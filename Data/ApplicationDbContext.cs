using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proje.Models;

namespace Proje.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Calisanlar> Calisanlar { get; set; }
        public DbSet<CalisanGelir> CalisanGelir { get; set; }
        public DbSet<Musteriler> Musteriler { get; set; }
        public DbSet<Randevular> Randevular { get; set; }
        public DbSet<Salon> Salon { get; set; }
        public DbSet<UzmanlikAlanlari> UzmanlikAlanlari { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UzmanlikAlanlari>().HasData(
                new UzmanlikAlanlari { ID = 1, ad = "Saç Kesimi", sure = 30, fiyat = 500 },
                new UzmanlikAlanlari { ID = 2, ad = "Saç Boyama", sure = 3, fiyat = 4000 },
                new UzmanlikAlanlari { ID = 3, ad = "Saç Şekillendirme", sure = 30, fiyat = 400 },
                new UzmanlikAlanlari { ID = 4, ad = "Makyaj ", sure = 1, fiyat = 600 }
            );

            modelBuilder.Entity<Randevular>()
                        .HasOne(r => r.calisan)
                        .WithMany(c => c.Randevular)
                        .HasForeignKey(r => r.calisan_ID)
                        .OnDelete(DeleteBehavior.Cascade);       
        }
    }
}