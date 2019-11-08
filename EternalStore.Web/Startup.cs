using EternalStore.ApplicationLogic.OrderManagement;
using EternalStore.ApplicationLogic.OrderManagement.Interfaces;
using EternalStore.ApplicationLogic.StoreManagement;
using EternalStore.ApplicationLogic.StoreManagement.Interfaces;
using EternalStore.ApplicationLogic.UserManagement;
using EternalStore.ApplicationLogic.UserManagement.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EternalStore.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json");
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddTransient<IStoreManager>(sm => new StoreManager(connection));
            services.AddTransient<IUserManager>(um => new UserManager(connection));
            services.AddTransient<IOrderManager>(om => new OrderManager(connection));
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Zdarova Banditi");
            //});
        }
    }
}
