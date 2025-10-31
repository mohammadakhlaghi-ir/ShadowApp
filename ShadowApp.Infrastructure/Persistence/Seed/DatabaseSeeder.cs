using ShadowApp.Application.Utilities;
using ShadowApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ShadowApp.Infrastructure.Persistence.Seed
{
    public static class DatabaseSeeder
    {
        public static void SeedAdminUser(this AppDbContext context)
        {
            context.Database.Migrate();

            if (!context.Users.Any())
            {
                var adminUser = new User
                {
                    Name = "admin",
                    Password = "Admin@123",
                    Email = "admin@example.com",
                    FirstName = "System",
                    LastName = "Admin"
                };

                adminUser.Crc = CrcHelper.ComputeCrc($"{adminUser.Name}|{adminUser.Email}|{adminUser.FirstName}" +
                    $"|{adminUser.LastName}|{adminUser.IsDeleted}|{adminUser.Enabled}|{adminUser.CreateDate:O}" +
                    $"|{adminUser.Creator}|{adminUser.ModifyDate:O}" + $"|{adminUser.Modifier}");

                context.Users.Add(adminUser);
                context.SaveChanges();
            }

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
                {
                    defaultLanguageID = existFarsiLanguage.ID;
                }

                var setting = new Setting
                {
                    LanguageID = defaultLanguageID,                    
                };

                setting.Crc = CrcHelper.ComputeCrc($"{setting.LanguageID}|{setting.ModifyDate:O}");

                context.Settings.Add(setting);
                context.SaveChanges();
            }
        }
    }
}