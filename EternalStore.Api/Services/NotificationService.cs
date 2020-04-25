using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EternalStore.ApplicationLogic.NotificationManagement.Interfaces;
using EternalStore.ApplicationLogic.UserManagement.DTO;
using EternalStore.ApplicationLogic.UserManagement.Interfaces;
using EternalStore.Domain.NotificationManagement;
using Microsoft.Extensions.Hosting;

namespace EternalStore.Api.Services
{
    public class NotificationService : BackgroundService
    {
        private readonly IScheduleManager scheduleManager;
        private readonly IUserManager userManager;

        private IEnumerable<SchedulerItem> schedulerItems;
        private IEnumerable<UserDTO> recipients;

        public NotificationService(IScheduleManager scheduleManager, IUserManager userManager)
        {
            this.scheduleManager = scheduleManager;
            this.userManager = userManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await RefreshData();

                if (schedulerItems.Any())
                {
                    foreach (var scheduler in schedulerItems)
                    {
                        await scheduleManager.SchedulerItemSetExecutionTime(scheduler.Id);
                        if (scheduler.ExecutionDateTime >= DateTime.Now.AddMinutes(1)) continue;

                        Task.Run(() => SendStatisticSchedulerAction(scheduler));

                        await scheduleManager.SchedulerItemRefreshTime(scheduler.Id);
                    }
                }

                await Task.Delay(60000, stoppingToken);
            }
        }

        private async Task RefreshData()
        {
            var allSchedulers = await scheduleManager.GetAllSchedulerItems();
            schedulerItems = allSchedulers.Where(si => si.IsActive);

            recipients = await userManager.GetAllUsersAsync();
        }

        private Action SendStatisticSchedulerAction(SchedulerItem scheduler) => () =>
        {
            foreach (var recipient in recipients)
            {
                //mailManager.SendEmailAsync(recipient.UserInformation.FirstName, recipient.UserInformation.Email, scheduler.Message);
            }
        };
    }
}
