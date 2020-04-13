using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EternalStore.ApplicationLogic.NotificationManagement.DTO;
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

        public async Task<int> CreateSchedulerItemAsync(SchedulerItemDTO schedulerItemDTO)
        {
            var schedulerItemsWithSameName = await schedulerItemRepository.GetByAsync(s => s.Name == schedulerItemDTO.Name);

            if (schedulerItemsWithSameName.Any())
                throw new Exception("Scheduler with same name already exists.");

            var schedulerItem = SchedulerItem.Insert(schedulerItemDTO.Name,
                schedulerItemDTO.MessageSubject,
                schedulerItemDTO.MessageBody,
                schedulerItemDTO.ExecutionFrequency,
                schedulerItemDTO.ExecutionHours,
                schedulerItemDTO.ExecutionMinutes,
                schedulerItemDTO.ExecutionDayOfWeek);

            await schedulerItemRepository.InsertAsync(schedulerItem);
            await schedulerItemRepository.SaveChangesAsync();

            return schedulerItem.Id;
        }

        public async Task UpdateSchedulerItemAsync(SchedulerItemDTO schedulerItemDTO)
        {
            var schedulerItem = await schedulerItemRepository.GetAsync(schedulerItemDTO.IdSchedulerItem);

            schedulerItem.Modify(schedulerItemDTO.Name);
            schedulerItem.Settings.Modify(schedulerItemDTO.ExecutionFrequency,
                schedulerItemDTO.ExecutionHours,
                schedulerItemDTO.ExecutionMinutes,
                schedulerItemDTO.ExecutionDayOfWeek);
            schedulerItem.Message.Modify(schedulerItemDTO.MessageSubject, schedulerItemDTO.MessageBody);

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
