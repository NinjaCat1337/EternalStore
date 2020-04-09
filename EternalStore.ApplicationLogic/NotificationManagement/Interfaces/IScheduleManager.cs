using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EternalStore.Domain.NotificationManagement;

namespace EternalStore.ApplicationLogic.NotificationManagement.Interfaces
{
    public interface IScheduleManager
    {
        Task<IEnumerable<SchedulerItem>> GetAllSchedulerItems();
        Task<SchedulerItem> GetSchedulerItem(int idSchedulerItem);
        Task<int> CreateSchedulerItemAsync(string name, string messageHeader, string messageBody,
            ExecutionFrequency executionFrequency, int executionHours, int executionMinutes, DayOfWeek? dayOfWeek = null);
        Task UpdateSchedulerItemAsync(int idScheduler, string name, string messageHeader, string messageBody,
            ExecutionFrequency executionFrequency, int executionHours, int executionMinutes, DayOfWeek? executionDayOfWeek = null);
        Task RunSchedulerItemAsync(int idSchedulerItem);
        Task StopSchedulerItemAsync(int idSchedulerItem);
        Task DeleteSchedulerItemAsync(int idSchedulerItem);
        Task SchedulerItemSetExecutionTime(int idScheduler);
        Task SchedulerItemRefreshTime(int idScheduler);
    }
}
