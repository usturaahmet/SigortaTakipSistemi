using Microsoft.EntityFrameworkCore;
using SigortaTakipSistemi.Models;

namespace SigortaTakipSistemi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Police> Policeler => Set<Police>();
        public DbSet<SigortaSirketi> SigortaSirketleri => Set<SigortaSirketi>();
        public DbSet<PoliceTuru> PoliceTurleri => Set<PoliceTuru>();
        public DbSet<Kullanici> Kullanicilar => Set<Kullanici>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Police>()
                .Property(p => p.Fiyat)
                .HasPrecision(18, 2); // Decimal güvenliği
        }
    }
}
