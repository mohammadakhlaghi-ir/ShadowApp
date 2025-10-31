using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ShadowApp.Application.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddApplicationMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}
