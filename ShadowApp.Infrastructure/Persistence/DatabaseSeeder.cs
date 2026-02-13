using ShadowApp.Application.Utilities;
using ShadowApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ShadowApp.Infrastructure.Persistence
{
    public static class DatabaseSeeder
    {
        public static readonly string appName = "Shadow App";
        public static readonly string[] initializePages = ["Home", "Dashboard"];

        public static void InitializeDatabase(this AppDbContext context)
        {
            context.Database.Migrate();
            context.SeedLanguages();
            context.SeedSetting();
            context.SeedAdminUser();
            foreach (var pageName in initializePages) context.SeedPages(pageName);
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
                    $"{farsiLanguage.Name}|{farsiLanguage.Description}|" +
                    $"{farsiLanguage.ModifyDate:O}|{farsiLanguage.Modifier}");

                var englishLanguage = new Language
                {
                    Name = "en",
                    Description = "English"
                };

                englishLanguage.Crc = CrcHelper.ComputeCrc(
                    $"{englishLanguage.Name}|{englishLanguage.Description}|" +
                    $"{englishLanguage.ModifyDate:O}|{englishLanguage.Modifier}");

                context.Languages.AddRange(farsiLanguage, englishLanguage);
                context.SaveChanges();
            }
        }

        public static void SeedSetting(this AppDbContext context)
        {
            if (!context.Settings.Any())
            {
                Guid defaultLanguageID;

                var existFarsiLanguage = context.Languages.FirstOrDefault(l => l.Name == "fa");

                if (existFarsiLanguage == null)
                {
                    var farsiLanguage = new Language
                    {
                        Name = "fa",
                        Description = "Farsi"
                    };

                    farsiLanguage.Crc = CrcHelper.ComputeCrc($"{farsiLanguage.Name}|" +
                        $"{farsiLanguage.Description}|{farsiLanguage.ModifyDate:O}|{farsiLanguage.Modifier}");

                    context.Languages.AddRange(farsiLanguage);
                    context.SaveChanges();

                    defaultLanguageID = farsiLanguage.ID;
                }
                else
                    defaultLanguageID = existFarsiLanguage.ID;

                var existFavicon = context.Favicons.FirstOrDefault();

                if (existFavicon == null)
                {
                    context.SeedFavicon();
                    existFavicon = context.Favicons.First();
                }

                var existLayout = context.Layouts.FirstOrDefault();
                if (existLayout == null)
                {
                    context.SeedLayout();
                    existLayout = context.Layouts.First();
                }

                var existLogo = context.Logos.FirstOrDefault();
                if (existLogo == null)
                {
                    context.SeedLogo();
                    existLogo = context.Logos.First();
                }

                var setting = new Setting
                {
                    LanguageID = defaultLanguageID,
                    AppName = appName,
                    FaviconID = existFavicon.ID,
                    LayoutID = existLayout.ID,
                    LogoID = existLogo.ID
                };

                setting.Crc = CrcHelper.ComputeCrc($"{setting.LanguageID}|{setting.AppName}|" +
                    $"{setting.FaviconID}|{setting.LayoutID}|{setting.Modifier}|{setting.ModifyDate:O}");

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

                var fa = context.Languages.First(l => l.Name == "fa");
                var en = context.Languages.First(l => l.Name == "en");

                var faTranslation = new UserTranslation
                {
                    User = adminUser,
                    LanguageID = fa.ID,
                    FirstName = "سیستم",
                    LastName = "مدیر",
                    Description = "مدیر سیستم"
                };

                var enTranslation = new UserTranslation
                {
                    User = adminUser,
                    LanguageID = en.ID,
                    FirstName = "System",
                    LastName = "Admin",
                    Description = "Administrator"
                };

                context.Users.Add(adminUser);
                context.UserTranslations.AddRange(faTranslation, enTranslation);

                adminUser.Crc = CrcHelper.ComputeCrc(
                    $"{adminUser.Name}|{adminUser.Email}|" +
                    $"{adminUser.IsDeleted}|{adminUser.Enabled}|" +
                    $"{adminUser.ModifyDate:O}|{adminUser.Modifier}");

                faTranslation.Crc = CrcHelper.ComputeCrc(
                    $"{faTranslation.UserID}|{faTranslation.LanguageID}|" +
                    $"{faTranslation.FirstName}|{faTranslation.LastName}|{faTranslation.Description}|" +
                    $"{faTranslation.ModifyDate:O}|{faTranslation.Modifier}");

                enTranslation.Crc = CrcHelper.ComputeCrc(
                    $"{enTranslation.UserID}|{enTranslation.LanguageID}|" +
                    $"{enTranslation.FirstName}|{enTranslation.LastName}|{enTranslation.Description}|" +
                    $"{enTranslation.ModifyDate:O}|{enTranslation.Modifier}");

                context.SaveChanges();
            }
        }

        public static void SeedFavicon(this AppDbContext context)
        {
            if (!context.Favicons.Any())
            {
                var favicon = new Favicon
                {
                    Name = "Main Favicon",
                    Description = "Main Favicon"
                };

                favicon.Crc = CrcHelper.ComputeCrc(
                    $"{favicon.Name}|{favicon.Description}|{favicon.ModifyDate:O}|{favicon.Modifier}");

                context.Favicons.Add(favicon);
                context.SaveChanges();
            }
        }

        public static void SeedLogo(this AppDbContext context)
        {
            if (!context.Logos.Any())
            {
                var logo = new Logo
                {
                    Name = "Main Logo",
                    Description = "Main Logo"
                };

                logo.Crc = CrcHelper.ComputeCrc(
                    $"{logo.Name}|{logo.Description}|{logo.ModifyDate:O}|{logo.Modifier}");

                context.Logos.Add(logo);
                context.SaveChanges();
            }
        }

        public static void SeedHeader(this AppDbContext context)
        {
            if (!context.Headers.Any())
            {
                var header = new Header
                {
                    Name = "Main Header",
                    Description = "Main Header"
                };

                header.Crc = CrcHelper.ComputeCrc(
                    $"{header.Name}|{header.Description}|{header.ModifyDate:O}|{header.Modifier}");

                context.Headers.Add(header);
                context.SaveChanges();
            }
        }

        public static void SeedLayout(this AppDbContext context)
        {
            if (!context.Layouts.Any())
            {
                var existHeader = context.Headers.FirstOrDefault();
                if (existHeader == null)
                {
                    context.SeedHeader();
                    existHeader = context.Headers.First();
                }

                var layout = new Layout
                {
                    Name = "Main Layout",
                    Description = "Main Layout",
                    HeaderID = existHeader.ID
                };

                layout.Crc = CrcHelper.ComputeCrc(
                    $"{layout.Name}|{layout.Description}|{existHeader.ID}|{layout.ModifyDate:O}|{layout.Modifier}");

                context.Layouts.Add(layout);
                context.SaveChanges();
            }
        }

        private static (string faName, string faDesc, string enName, string enDesc) GetPageTranslations(string pageName)
        {
            return pageName switch
            {
                "Home" => (
                    "خانه",
                    "صفحه اصلی سایت",
                    "Home",
                    "Main Page Of Site"
                ),
                "Dashboard" => (
                    "داشبورد",
                    "صفحه داشبورد کاربران",
                    "Dashboard",
                    "Dashboard Page Of Users"
                ),
                _ => (
                    pageName,
                    pageName,
                    pageName,
                    pageName
                )
            };
        }

        public static void SeedSpecialPage(this AppDbContext context, string pageName)
        {
            if (context.SpecialPages.Any(s => s.Name == pageName))
                return;

            var (faName, faDesc, enName, enDesc) = GetPageTranslations(pageName);

            var fa = context.Languages.First(l => l.Name == "fa");
            var en = context.Languages.First(l => l.Name == "en");

            var specialPage = new SpecialPage
            {
                Name = pageName
            };

            var faTranslation = new SpecialPageTranslation
            {
                SpecialPage = specialPage,
                LanguageID = fa.ID,
                Name = faName,
                Description = faDesc
            };

            var enTranslation = new SpecialPageTranslation
            {
                SpecialPage = specialPage,
                LanguageID = en.ID,
                Name = enName,
                Description = enDesc
            };

            context.SpecialPages.Add(specialPage);
            context.SpecialPageTranslations.AddRange(faTranslation, enTranslation);

            specialPage.Crc = CrcHelper.ComputeCrc(
                $"{specialPage.Name}|{specialPage.ModifyDate:O}|{specialPage.Modifier}");

            faTranslation.Crc = CrcHelper.ComputeCrc(
                $"{faTranslation.Name}|{faTranslation.Description}|{faTranslation.LanguageID}|" +
                $"{faTranslation.ModifyDate:O}|{faTranslation.Modifier}");

            enTranslation.Crc = CrcHelper.ComputeCrc(
                $"{enTranslation.Name}|{enTranslation.Description}|{enTranslation.LanguageID}|" +
                $"{enTranslation.ModifyDate:O}|{enTranslation.Modifier}");

            context.SaveChanges();
        }

        public static void SeedPages(this AppDbContext context, string pageName)
        {
            var specialPage = context.SpecialPages.FirstOrDefault(s => s.Name == pageName);

            if (specialPage == null)
            {
                context.SeedSpecialPage(pageName);
                specialPage = context.SpecialPages.First(s => s.Name == pageName);
            }

            if (context.Pages.Any(p => p.SpecialPageID == specialPage.ID))
                return;

            var setting = context.Settings.FirstOrDefault();

            if (setting == null)
            {
                context.SeedSetting();
                setting = context.Settings.First();
            }

            var (faName, faDesc, enName, enDesc) = GetPageTranslations(pageName);

            var fa = context.Languages.First(l => l.Name == "fa");
            var en = context.Languages.First(l => l.Name == "en");

            var page = new Page
            {
                SpecialPageID = specialPage.ID,
                FaviconID = setting.FaviconID,
                LayoutID = setting.LayoutID
            };

            var faTranslation = new PageTranslation
            {
                Page = page,
                LanguageID = fa.ID,
                Name = faName,
                Description = faDesc
            };

            var enTranslation = new PageTranslation
            {
                Page = page,
                LanguageID = en.ID,
                Name = enName,
                Description = enDesc
            };

            context.Pages.Add(page);
            context.PageTranslations.AddRange(faTranslation, enTranslation);

            page.Crc = CrcHelper.ComputeCrc(
                $"{page.SpecialPageID}|{page.FaviconID}|{page.LayoutID}|{page.HeaderVisible}" +
                $"|{page.ModifyDate:O}|{page.Modifier}");

            faTranslation.Crc = CrcHelper.ComputeCrc(
                $"{faTranslation.Name}|{faTranslation.Description}|{faTranslation.LanguageID}|" +
                $"{faTranslation.ModifyDate:O}|{faTranslation.Modifier}");

            enTranslation.Crc = CrcHelper.ComputeCrc(
                $"{enTranslation.Name}|{enTranslation.Description}|{enTranslation.LanguageID}|" +
                $"{enTranslation.ModifyDate:O}|{enTranslation.Modifier}");

            context.SaveChanges();
        }
    }
}