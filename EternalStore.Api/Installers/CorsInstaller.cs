using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EternalStore.Api.Installers
{
    public class CorsInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(configuration["CorsHeaderName"],
                    builder =>
                    {
                        builder.WithOrigins(configuration["AllowedOrigins:EternalStore.Client"])
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }
    }
}
