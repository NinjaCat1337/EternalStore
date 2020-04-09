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

        public async Task<IEnumerable<SchedulerItem>> GetAllSchedulerItems() =>
            await schedulerItemRepository.GetAll().Where(si => !si.IsDeleted).ToListAsync();

        public async Task<SchedulerItem> GetSchedulerItem(int idSchedulerItemItem) => await schedulerItemRepository.GetAsync(idSchedulerItemItem);

        public async Task<int> CreateSchedulerItemAsync(string name, string messageSubject, string messageBody, ExecutionFrequency executionFrequency, int executionHours, int executionMinutes,
            DayOfWeek? dayOfWeek = null)
        {
            var schedulerItemsWithSameName = await schedulerItemRepository.GetByAsync(s => s.Name == name);

            if (schedulerItemsWithSameName.Any())
                throw new Exception("Scheduler with same name already exists.");

            var schedulerItem = SchedulerItem.Insert(name, messageSubject, messageBody, executionFrequency, executionHours, executionMinutes, dayOfWeek);

            await schedulerItemRepository.InsertAsync(schedulerItem);
            await schedulerItemRepository.SaveChangesAsync();

            return schedulerItem.Id;
        }

        public async Task UpdateSchedulerItemAsync(int idSchedulerItem, string name, string messageHeader, string messageBody, ExecutionFrequency executionFrequency, int executionHours, int executionMinutes, DayOfWeek? executionDayOfWeek = null)
        {
            var schedulerItem = await schedulerItemRepository.GetAsync(idSchedulerItem);
            schedulerItem.Modify(name);
            schedulerItem.Settings.Modify(executionFrequency, executionHours, executionMinutes, executionDayOfWeek);
            schedulerItem.Message.Modify(messageHeader, messageBody);

            await schedulerItemRepository.SaveChangesAsync();
        }

        public async Task RunSchedulerItemAsync(int idSchedulerItem)
        {
            var schedulerItem = await schedulerItemRepository.GetAsync(idSchedulerItem);
            schedulerItem.Run();

            await schedulerItemRepository.SaveChangesAsync();
        }

        public async Task StopSchedulerItemAsync(int idSchedulerItem)
        {
            var schedulerItem = await schedulerItemRepository.GetAsync(idSchedulerItem);
            schedulerItem.Stop();

            await schedulerItemRepository.SaveChangesAsync();
        }

        public async Task DeleteSchedulerItemAsync(int idSchedulerItem)
        {
            var schedulerItem = await schedulerItemRepository.GetAsync(idSchedulerItem);
            schedulerItem.Delete();

            await schedulerItemRepository.SaveChangesAsync();
        }

        public async Task SchedulerItemSetExecutionTime(int idSchedulerItem)
        {
            var schedulerItem = await schedulerItemRepository.GetAsync(idSchedulerItem);
            schedulerItem.SetExecutionDateTime();

            await schedulerItemRepository.SaveChangesAsync();
        }

        public async Task SchedulerItemRefreshTime(int idSchedulerItem)
        {
            var schedulerItem = await schedulerItemRepository.GetAsync(idSchedulerItem);
            schedulerItem.RefreshExecutionDateTime();

            await schedulerItemRepository.SaveChangesAsync();
        }
    }
}
