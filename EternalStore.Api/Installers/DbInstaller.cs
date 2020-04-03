using EternalStore.ApplicationLogic.NotificationManagement;
using EternalStore.ApplicationLogic.NotificationManagement.Interfaces;
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
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var apiKey = configuration["JwtSettings:ApiKey"];

            services.AddTransient<IGoodsManager>(sm => new GoodsManager(connectionString));
            services.AddTransient<IUserManager>(um => new UserManager(connectionString, apiKey));
            services.AddTransient<IOrderManager>(om => new OrderManager(connectionString));
            services.AddTransient<IStatisticManager>(sm => new StatisticManager(connectionString));
            services.AddTransient<IMailManager>(mm => new MailManager(configuration));
            services.AddTransient<IScheduleManager>(sm => new ScheduleManager(configuration));
        }
    }
}
