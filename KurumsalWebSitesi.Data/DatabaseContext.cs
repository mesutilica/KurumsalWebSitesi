using KurumsalWebSitesi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace KurumsalWebSitesi.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // lokal db bağlantı
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=KurumsalWebSitesi; integrated security=true; TrustServerCertificate=True;").ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
            // canlı db bağlantı
            // optionsBuilder.UseSqlServer("Server=84.18.158.34; Database=KurumsalWebSitesi; username=canlı db kullancı adı; password=canlı db şifre; TrustServerCertificate=True;").ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Admin",
                    Surname = "User",
                    CreateDate = DateTime.Now,
                    Email = "Test@KurumsalWebSitesi.co",
                    IsActive = true,
                    IsAdmin = true,
                    Password = "Test123"
                }
                );
        }
    }
}
