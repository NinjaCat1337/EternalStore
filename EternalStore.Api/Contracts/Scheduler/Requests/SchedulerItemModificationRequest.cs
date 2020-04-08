using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EternalStore.Domain.NotificationManagement;

namespace EternalStore.Api.Contracts.Scheduler.Requests
{
    public class SchedulerItemModificationRequest
    {
        public int IdSchedulerItem { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public ExecutionFrequency ExecutionFrequency { get; set; }
        public int ExecutionHours { get; set; }
        public int ExecutionMinutes { get; set; }
        public DayOfWeek ExecutionDayOfWeek { get; set; }
    }
}
