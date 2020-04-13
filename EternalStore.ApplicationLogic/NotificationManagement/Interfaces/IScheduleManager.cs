using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EternalStore.ApplicationLogic.NotificationManagement.DTO;
using EternalStore.Domain.NotificationManagement;

namespace EternalStore.ApplicationLogic.NotificationManagement.Interfaces
{
    public interface IScheduleManager
    {
        Task<IEnumerable<SchedulerItem>> GetAllSchedulerItems();
        Task<SchedulerItem> GetSchedulerItem(int idSchedulerItem);
        Task<int> CreateSchedulerItemAsync(SchedulerItemDTO schedulerItemDTO);
        Task UpdateSchedulerItemAsync(SchedulerItemDTO schedulerItemDTO);
        Task RunSchedulerItemAsync(int idSchedulerItem);
        Task StopSchedulerItemAsync(int idSchedulerItem);
        Task DeleteSchedulerItemAsync(int idSchedulerItem);
        Task SchedulerItemSetExecutionTime(int idScheduler);
        Task SchedulerItemRefreshTime(int idScheduler);
    }
}
