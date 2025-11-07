using Microsoft.EntityFrameworkCore;
using ShadowApp.Domain.Entities;

namespace ShadowApp.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Log> Logs => Set<Log>();
        public DbSet<Setting> Settings => Set<Setting>();
        public DbSet<Language> Languages => Set<Language>();
        public DbSet<UserTranslation> UserTranslations => Set<UserTranslation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Setting>()
                .HasOne(s => s.Language)
                .WithOne(l => l.Setting)
                .HasForeignKey<Setting>(s => s.LanguageID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserTranslation>()
                .HasIndex(t => new { t.UserID, t.LanguageID })
                .IsUnique();

            modelBuilder.Entity<UserTranslation>()
                .HasOne(t => t.User)
                .WithMany(u => u.Translations)
                .HasForeignKey(t => t.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserTranslation>()
                .HasOne(t => t.Language)
                .WithMany()
                .HasForeignKey(t => t.LanguageID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}