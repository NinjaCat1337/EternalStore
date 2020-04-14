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
            services.AddTransient<IGoodsManager>(sm => new GoodsManager(configuration));
            services.AddSingleton<IUserManager>(um => new UserManager(configuration));
            services.AddSingleton<IOrderManager>(om => new OrderManager(configuration));
            services.AddSingleton<IStatisticManager>(sm => new StatisticManager(configuration));
            services.AddSingleton<IMailManager>(mm => new MailManager(configuration));
            services.AddSingleton<IScheduleManager>(sm => new ScheduleManager(configuration));
        }
    }
}
