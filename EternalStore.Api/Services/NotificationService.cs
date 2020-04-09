using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EternalStore.ApplicationLogic.NotificationManagement.Interfaces;
using EternalStore.ApplicationLogic.StoreManagement.Interfaces;
using EternalStore.ApplicationLogic.UserManagement.DTO;
using EternalStore.ApplicationLogic.UserManagement.Interfaces;
using EternalStore.Domain.NotificationManagement;
using Microsoft.Extensions.Hosting;

namespace EternalStore.Api.Services
{
    public class NotificationService : BackgroundService
    {
        private readonly IStatisticManager statisticManager;
        private readonly IMailManager mailManager;
        private readonly IScheduleManager scheduleManager;
        private readonly IUserManager userManager;

        private IEnumerable<SchedulerItem> schedulerItems;
        private IEnumerable<UserDTO> recipients;

        public NotificationService(IStatisticManager statisticManager, IMailManager mailManager, IScheduleManager scheduleManager, IUserManager userManager)
        {
            this.statisticManager = statisticManager;
            this.mailManager = mailManager;
            this.scheduleManager = scheduleManager;
            this.userManager = userManager;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken) =>
            Task.Run(() =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    LoadData();
                    foreach (var scheduler in schedulerItems)
                    {
                        Task.Run(() =>
                        {
                            Thread.Sleep((int)scheduler.ExecutionDateTime.Subtract(DateTime.Now).TotalMilliseconds);
                            SendStatisticSchedulerAction(scheduler);
                            scheduleManager.SchedulerItemRefreshTime(scheduler.Id);
                        });
                    }
                }
            });

        private async void LoadData()
        {
            var allSchedulers = await scheduleManager.GetAllSchedulerItems();
            schedulerItems = allSchedulers.Where(si => si.IsActive);
            recipients = await userManager.GetAllUsersAsync();
        }

        private Action SendStatisticSchedulerAction(SchedulerItem scheduler) => () =>
        {
            foreach (var recipient in recipients)
            {
                mailManager.SendEmailAsync(recipient.UserInformation.FirstName, recipient.UserInformation.Email, scheduler.Message);
            }
        };
    }
}
