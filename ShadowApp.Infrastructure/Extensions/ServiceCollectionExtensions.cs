using ShadowApp.Application.Interfaces;
using ShadowApp.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ShadowApp.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            services.AddScoped<ILogService, LogService>();
            services.AddSingleton<ITranslationService, TranslationService>();
            return services;
        }
    }
}
