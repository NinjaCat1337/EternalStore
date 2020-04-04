using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EternalStore.Domain.NotificationManagement;

namespace EternalStore.ApplicationLogic.NotificationManagement.Interfaces
{
    public interface IScheduleManager
    {
        Task<IEnumerable<SchedulerItem>> GetAllSchedulerItems();

        Task<int> CreateSchedulerItemAsync(string name, string messageHeader, string messageBody,
            ExecutionFrequency executionFrequency, int executionHours, int executionMinutes, DayOfWeek? dayOfWeek = null);

        Task UpdateSchedulerItemAsync(int idScheduler, string name, string messageHeader, string messageBody,
            ExecutionFrequency executionFrequency, int executionHours, int executionMinutes, DayOfWeek? executionDayOfWeek = null);

        Task SchedulerItemSetExecutionTime(int idScheduler);

        Task SchedulerItemRefreshTime(int idScheduler);
    }
}
