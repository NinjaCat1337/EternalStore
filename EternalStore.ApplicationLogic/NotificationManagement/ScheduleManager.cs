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
        private readonly SchedulerRepository schedulerRepository;
        public ScheduleManager(IConfiguration configuration)
        {
            schedulerRepository = new SchedulerRepository(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<Scheduler>> GetAllSchedulers() => await schedulerRepository.GetAll().ToListAsync();

        public async Task<int> CreateSchedulerAsync(string name, string messageHeader, string messageBody, ExecutionFrequency executionFrequency, int executionHours, int executionMinutes,
            DayOfWeek? dayOfWeek = null)
        {
            var schedulersWithSameName = await schedulerRepository.GetByAsync(s => s.Name == name);

            if (schedulersWithSameName.Any())
                throw new Exception("Scheduler with same name already exists.");

            var scheduler = Scheduler.Insert(name, messageHeader, messageBody, executionFrequency, executionHours, executionMinutes, dayOfWeek);

            await schedulerRepository.InsertAsync(scheduler);
            await schedulerRepository.SaveChangesAsync();

            return scheduler.Id;
        }

        public async Task UpdateSchedulerAsync(int idScheduler, string name, string messageHeader, string messageBody, ExecutionFrequency executionFrequency, int executionHours, int executionMinutes, DayOfWeek? executionDayOfWeek = null)
        {
            var scheduler = await schedulerRepository.GetAsync(idScheduler);
            scheduler.Modify(name);
            scheduler.Settings.Modify(executionFrequency, executionHours, executionMinutes, executionDayOfWeek);
            scheduler.Message.Modify(messageHeader, messageBody);

            await schedulerRepository.SaveChangesAsync();
        }

        public async Task SchedulerSetExecutionTime(int idScheduler)
        {
            var scheduler = await schedulerRepository.GetAsync(idScheduler);
            scheduler.SetExecutionDateTime();

            await schedulerRepository.SaveChangesAsync();
        }

        public async Task SchedulerRefreshTime(int idScheduler)
        {
            var scheduler = await schedulerRepository.GetAsync(idScheduler);
            scheduler.RefreshExecutionDateTime();

            await schedulerRepository.SaveChangesAsync();
        }
    }
}
