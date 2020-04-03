using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EternalStore.Domain.NotificationManagement;

namespace EternalStore.ApplicationLogic.NotificationManagement.Interfaces
{
    public interface IScheduleManager
    {
        Task<IEnumerable<Scheduler>> GetAllSchedulers();

        Task<int> CreateSchedulerAsync(string name, string messageHeader, string messageBody,
            ExecutionFrequency executionFrequency, int executionHours, int executionMinutes, DayOfWeek? dayOfWeek = null);

        Task UpdateSchedulerAsync(int idScheduler, string name, string messageHeader, string messageBody,
            ExecutionFrequency executionFrequency, int executionHours, int executionMinutes, DayOfWeek? executionDayOfWeek = null);

        Task SchedulerSetExecutionTime(int idScheduler);

        Task SchedulerRefreshTime(int idScheduler);
    }
}
