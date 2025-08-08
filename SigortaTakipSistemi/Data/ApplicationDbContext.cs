using Microsoft.EntityFrameworkCore;
using SigortaTakipSistemi.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace YourNamespace.Data // namespace'ini kendi projenle uyumlu yap
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Police> Policeler { get; set; }
        public DbSet<PoliceTuru> PoliceTurleri { get; set; }
        public DbSet<SigortaSirketi> SigortaSirketleri { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Police>()
                .Property(p => p.TcNo)
                .HasMaxLength(11);

            modelBuilder.Entity<Kullanici>()
                .HasIndex(k => k.KullaniciAdi)
                .IsUnique();

            modelBuilder.Entity<Police>()
        .HasOne(p => p.SigortaSirketi)
        .WithMany()
        .HasForeignKey(p => p.SigortaSirketiId)
        .OnDelete(DeleteBehavior.Restrict); // ❗ CASCADE kaldırıldı

            modelBuilder.Entity<Police>()
                .HasOne(p => p.PoliceTuru)
                .WithMany()
                .HasForeignKey(p => p.PoliceTuruId)
                .OnDelete(DeleteBehavior.Restrict); // ❗ CASCADE kaldırıldı

            modelBuilder.Entity<Police>()
                .HasOne(p => p.Personel)
                .WithMany()
                .HasForeignKey(p => p.PersonelId)
                .OnDelete(DeleteBehavior.Restrict); // ❗ CASCADE kaldırıldı

            // TCNo örneği (önceden vardı)
            modelBuilder.Entity<Police>()
                .Property(p => p.TcNo)
                .HasMaxLength(11);

            modelBuilder.Entity<Police>()
    .Property(p => p.Fiyat)
    .HasPrecision(18, 2); // Örn: 12345678.90

            modelBuilder.Entity<PoliceTuru>()
                .Property(p => p.AcentaPrimi)
                .HasPrecision(5, 2); // Örn: %30.75

            modelBuilder.Entity<PoliceTuru>()
                .Property(p => p.Prim)
                .HasPrecision(5, 2); // Örn: %70.00
        }
    }
}
