using EternalStore.ApplicationLogic.StoreManagement;
using EternalStore.ApplicationLogic.StoreManagement.Interfaces;
using EternalStore.ApplicationLogic.UserManagement;
using EternalStore.ApplicationLogic.UserManagement.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EternalStore.Api.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            var apiKey = configuration["JwtSettings:ApiKey"];

            services.AddTransient<IGoodsManager>(sm => new GoodsManager(connection));
            services.AddTransient<IUserManager>(um => new UserManager(connection, apiKey));
            services.AddTransient<IOrderManager>(om => new OrderManager(connection));
        }
    }
}
