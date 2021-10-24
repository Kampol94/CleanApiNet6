using CleanApi.Application.Settings;
using CleanApi.Infrastructure.Persistence.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CleanApi.Infrastructure.Extensions
{
    public static class CORSExtensions
    {
        internal static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            var corsSettings = services.GetOptions<CorsSettings>(nameof(CorsSettings));
            return services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(corsSettings.Angular);
                });
            });
        }
    }
}