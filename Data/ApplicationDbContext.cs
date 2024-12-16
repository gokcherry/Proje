using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebProgramlamaProje.Models;

namespace WebProgramlamaProje.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Calisanlar> Calisanlar { get; set; }
        public DbSet<CalisanGelir> CalisanGelir { get; set; }
        public DbSet<CalisanUzmanlik> CalisanUzmanlik { get; set; }
        public DbSet<Musteriler> Musteriler { get; set; }
        public DbSet<Randevular> Randevular { get; set; }
        public DbSet<Salon> Salon { get; set; }
        public DbSet<UzmanlikAlanlari> UzmanlikAlanlari { get; set; }
    }
}
