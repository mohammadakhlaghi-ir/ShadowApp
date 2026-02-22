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
        public DbSet<Favicon> Favicons => Set<Favicon>();
        public DbSet<Logo> Logos => Set<Logo>();
        public DbSet<Page> Pages => Set<Page>();
        public DbSet<PageTranslation> PageTranslations => Set<PageTranslation>();
        public DbSet<SpecialPage> SpecialPages => Set<SpecialPage>();
        public DbSet<SpecialPageTranslation> SpecialPageTranslations => Set<SpecialPageTranslation>();
        public DbSet<Layout> Layouts => Set<Layout>();
        public DbSet<Header> Headers => Set<Header>();
        public DbSet<HeaderSection> HeaderSections => Set<HeaderSection>();
        public DbSet<LayoutItem> LayoutItems => Set<LayoutItem>();
        public DbSet<LayoutItemCategory> LayoutItemCategories => Set<LayoutItemCategory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Setting
            modelBuilder.Entity<Setting>()
                .HasOne(s => s.Language)
                .WithOne(l => l.Setting)
                .HasForeignKey<Setting>(s => s.LanguageID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Setting>()
                .HasOne(s => s.Favicon)
                .WithOne(f => f.Setting)
                .HasForeignKey<Setting>(s => s.FaviconID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Setting>()
                .HasOne(s => s.Logo)
                .WithOne(l => l.Setting)
                .HasForeignKey<Setting>(s => s.LogoID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Setting>()
                .HasOne(s => s.Layout)
                .WithOne(l => l.Setting)
                .HasForeignKey<Setting>(s => s.LayoutID)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region UserTranslation
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
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Language
            modelBuilder.Entity<Language>()
                .Property(l => l.Name)
                .ValueGeneratedNever()
                .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
            #endregion

            #region Page
            modelBuilder.Entity<Page>()
              .HasOne(p => p.SpecialPage)
              .WithOne(s => s.Page)
              .HasForeignKey<Page>(p => p.SpecialPageID)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Page>()
                .HasOne(p => p.Favicon)
                .WithMany(f => f.Pages)
                .HasForeignKey(p => p.FaviconID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Page>()
                .HasOne(p => p.Layout)
                .WithMany(l => l.Pages)
                .HasForeignKey(p => p.LayoutID)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region PageTranslation
            modelBuilder.Entity<PageTranslation>()
                .HasIndex(t => new { t.PageID, t.LanguageID })
                .IsUnique();

            modelBuilder.Entity<PageTranslation>()
                .HasOne(t => t.Page)
                .WithMany(p => p.Translations)
                .HasForeignKey(t => t.PageID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PageTranslation>()
                .HasOne(t => t.Language)
                .WithMany()
                .HasForeignKey(t => t.LanguageID)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region SpecialPageTranslation
            modelBuilder.Entity<SpecialPageTranslation>()
                .HasIndex(t => new { t.SpecialPageID, t.LanguageID })
                .IsUnique();

            modelBuilder.Entity<SpecialPageTranslation>()
                .HasOne(t => t.SpecialPage)
                .WithMany(p => p.Translations)
                .HasForeignKey(t => t.SpecialPageID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SpecialPageTranslation>()
                .HasOne(t => t.Language)
                .WithMany()
                .HasForeignKey(t => t.LanguageID)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Layout
            modelBuilder.Entity<Layout>()
                .HasOne(p => p.Header)
                .WithMany(l => l.Layouts)
                .HasForeignKey(p => p.HeaderID)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region HeaderSection
            modelBuilder.Entity<HeaderSection>()
                .HasOne(p => p.Header)
                .WithMany(l => l.HeaderSections)
                .HasForeignKey(p => p.HeaderID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HeaderSection>()
                .HasOne(p => p.LayoutItem)
                .WithMany(l => l.HeaderSections)
                .HasForeignKey(p => p.LayoutItemID)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region LayoutItem
            modelBuilder.Entity<LayoutItem>()
                .HasOne(p => p.LayoutItemCategory)
                .WithMany(l => l.LayoutItems)
                .HasForeignKey(p => p.LayoutItemCategoryID)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion         
        }
    }
}