using ShadowApp.Application.Utilities;
using ShadowApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ShadowApp.Infrastructure.Persistence
{
    public static class DatabaseSeeder
    {
        public static void InitializeDatabase(this AppDbContext context)
        {
            context.Database.Migrate();
            context.SeedLanguages();
            context.SeedSetting();
            context.SeedAdminUser();
            context.CreateLanguageProtectionTrigger();
        }

        public static void SeedLanguages(this AppDbContext context)
        {
            if (!context.Languages.Any())
            {
                var farsiLanguage = new Language
                {
                    Name = "fa",
                    Description = "Farsi"
                };

                farsiLanguage.Crc = CrcHelper.ComputeCrc(
                    $"{farsiLanguage.Name}|" +
                    $"{farsiLanguage.Description}|{farsiLanguage.CreateDate:O}|{farsiLanguage.Creator}|" +
                    $"{farsiLanguage.ModifyDate:O}|{farsiLanguage.Modifier}");

                var englishLanguage = new Language
                {
                    Name = "en",
                    Description = "English"
                };

                englishLanguage.Crc = CrcHelper.ComputeCrc(
                    $"{englishLanguage.Name}|" +
                    $"{englishLanguage.Description}|{englishLanguage.CreateDate:O}|{englishLanguage.Creator}|" +
                    $"{englishLanguage.ModifyDate:O}|{englishLanguage.Modifier}");

                context.Languages.AddRange(farsiLanguage, englishLanguage);
                context.SaveChanges();
            }
        }

        public static void SeedSetting(this AppDbContext context)
        {
            if (!context.Settings.Any())
            {
                uint defaultLanguageID = 0;

                var existFarsiLanguage = context.Languages.FirstOrDefault(l => l.Name == "fa");

                if (existFarsiLanguage == null)
                {
                    var farsiLanguage = new Language
                    {
                        Name = "fa",
                        Description = "Farsi"
                    };

                    farsiLanguage.Crc = CrcHelper.ComputeCrc($"{farsiLanguage.Name}|" +
                        $"{farsiLanguage.Description}|{farsiLanguage.CreateDate:O}|{farsiLanguage.Creator}|" +
                        $"{farsiLanguage.ModifyDate:O}|{farsiLanguage.Modifier}");

                    context.Languages.AddRange(farsiLanguage);
                    context.SaveChanges();

                    defaultLanguageID = farsiLanguage.ID;
                }
                else
                    defaultLanguageID = existFarsiLanguage.ID;

                var existFavicon = context.Favicons.FirstOrDefault(f => f.Main);

                if (existFavicon == null)
                {
                    context.SeedFavicon();
                    existFavicon = context.Favicons.First(f => f.Main);
                }

                var setting = new Setting
                {
                    LanguageID = defaultLanguageID,
                    AppName = "Shadow App",
                    FaviconID = existFavicon.ID
                };

                setting.Crc = CrcHelper.ComputeCrc($"{setting.ID}|{setting.LanguageID}|{setting.AppName}|" +
                    $"{setting.AppDescription}|{setting.FaviconID}|{setting.Modifier}|{setting.ModifyDate:O}");

                context.Settings.Add(setting);
                context.SaveChanges();
            }
        }

        public static void SeedAdminUser(this AppDbContext context)
        {
            if (!context.Users.Any())
            {
                var adminUser = new User
                {
                    Name = "admin",
                    Password = "Admin@123",
                    Email = "admin@example.com",
                };
                context.Users.Add(adminUser);
                context.SaveChanges();

                var fa = context.Languages.First(l => l.Name == "fa");
                var en = context.Languages.First(l => l.Name == "en");

                var faTranslation = new UserTranslation
                {
                    UserID = adminUser.ID,
                    LanguageID = fa.ID,
                    FirstName = "سیستم",
                    LastName = "مدیر"
                };

                var enTranslation = new UserTranslation
                {
                    UserID = adminUser.ID,
                    LanguageID = en.ID,
                    FirstName = "System",
                    LastName = "Admin"
                };

                faTranslation.Crc = CrcHelper.ComputeCrc(
                    $"{faTranslation.UserID}|{faTranslation.LanguageID}|" +
                    $"{faTranslation.FirstName}|{faTranslation.LastName}|" +
                    $"{faTranslation.CreateDate:O}|{faTranslation.Creator}|" +
                    $"{faTranslation.ModifyDate:O}|{faTranslation.Modifier}"
                );

                enTranslation.Crc = CrcHelper.ComputeCrc(
                    $"{enTranslation.UserID}|{enTranslation.LanguageID}|" +
                    $"{enTranslation.FirstName}|{enTranslation.LastName}|" +
                    $"{enTranslation.CreateDate:O}|{enTranslation.Creator}|" +
                    $"{enTranslation.ModifyDate:O}|{enTranslation.Modifier}"
                );

                context.UserTranslations.AddRange(faTranslation, enTranslation);

                adminUser.Crc = CrcHelper.ComputeCrc(
                     $"{adminUser.Name}|{adminUser.Email}|" +
                     $"{adminUser.IsDeleted}|{adminUser.Enabled}|" +
                     $"{adminUser.CreateDate:O}|{adminUser.Creator}|" +
                     $"{adminUser.ModifyDate:O}|{adminUser.Modifier}"
                 );

                context.SaveChanges();
            }
        }

        public static void SeedFavicon(this AppDbContext context)
        {
            if (!context.Favicons.Any())
            {
                var favicon = new Favicon
                {
                    Name = "MainFavicon",
                    Main = true,
                };

                context.Favicons.Add(favicon);
                context.SaveChanges();

                favicon.Crc = CrcHelper.ComputeCrc(
                    $"{favicon.ID}|{favicon.Name}|" +
                    $"{favicon.Description}|{favicon.Main}|" +
                    $"|{favicon.Creator}|{favicon.CreateDate:O}" +
                    $"{favicon.ModifyDate:O}|{favicon.Modifier}"
                );

                context.Favicons.Update(favicon);
                context.SaveChanges();
            }
        }
    }
}