using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EternalStore.ApplicationLogic.NotificationManagement.Interfaces;
using EternalStore.DataAccess.NotificationManagement.Repositories;
using EternalStore.Domain.NotificationManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EternalStore.ApplicationLogic.NotificationManagement
{
    public class ScheduleManager : IScheduleManager
    {
        private readonly SchedulerItemRepository schedulerItemRepository;
        public ScheduleManager(IConfiguration configuration)
        {
            schedulerItemRepository = new SchedulerItemRepository(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<SchedulerItem>> GetAllSchedulerItems() => await schedulerItemRepository.GetAll().ToListAsync();

        public async Task<int> CreateSchedulerAsync(string name, string messageHeader, string messageBody, ExecutionFrequency executionFrequency, int executionHours, int executionMinutes,
            DayOfWeek? dayOfWeek = null)
        {
            var schedulerItemsWithSameName = await schedulerItemRepository.GetByAsync(s => s.Name == name);

            if (schedulerItemsWithSameName.Any())
                throw new Exception("Scheduler with same name already exists.");

            var schedulerItem = SchedulerItem.Insert(name, messageHeader, messageBody, executionFrequency, executionHours, executionMinutes, dayOfWeek);

            await schedulerItemRepository.InsertAsync(schedulerItem);
            await schedulerItemRepository.SaveChangesAsync();

            return schedulerItem.Id;
        }

        public async Task UpdateSchedulerAsync(int idScheduler, string name, string messageHeader, string messageBody, ExecutionFrequency executionFrequency, int executionHours, int executionMinutes, DayOfWeek? executionDayOfWeek = null)
        {
            var schedulerItem = await schedulerItemRepository.GetAsync(idScheduler);
            schedulerItem.Modify(name);
            schedulerItem.Settings.Modify(executionFrequency, executionHours, executionMinutes, executionDayOfWeek);
            schedulerItem.Message.Modify(messageHeader, messageBody);

            await schedulerItemRepository.SaveChangesAsync();
        }

        public async Task SchedulerSetExecutionTime(int idScheduler)
        {
            var schedulerItem = await schedulerItemRepository.GetAsync(idScheduler);
            schedulerItem.SetExecutionDateTime();

            await schedulerItemRepository.SaveChangesAsync();
        }

        public async Task SchedulerRefreshTime(int idScheduler)
        {
            var schedulerItem = await schedulerItemRepository.GetAsync(idScheduler);
            schedulerItem.RefreshExecutionDateTime();

            await schedulerItemRepository.SaveChangesAsync();
        }
    }
}
