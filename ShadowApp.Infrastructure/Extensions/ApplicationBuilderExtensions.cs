using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShadowApp.Application.DTOs;
using ShadowApp.Application.Interfaces;
using ShadowApp.Infrastructure.Persistence;
using ShadowApp.Infrastructure.Persistence.Seed;

namespace ShadowApp.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task UseInfrastructureSeedAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var logService = scope.ServiceProvider.GetRequiredService<ILogService>();

            var setting = await dbContext.Settings.Include(s => s.Language).FirstOrDefaultAsync();
            var langName = setting?.Language?.Name ?? "fa";

            try
            {
                dbContext.SeedAdminUser();

                await logService.AddLog(new AddLogDto
                {
                    Title = "ApplicationStartedTitle",
                    Description = "ApplicationStartedDescription"
                }, langName, new Dictionary<string, string>
                {
                    ["time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                });
            }
            catch (Exception ex)
            {
                await logService.AddLog(new AddLogDto
                {
                    Title = "StartupErrorTitle",
                    Description = "StartupErrorDescription"
                }, langName, new Dictionary<string, string>
                {
                    ["error"] = ex.Message
                });
                throw;
            }
        }
    }
}
