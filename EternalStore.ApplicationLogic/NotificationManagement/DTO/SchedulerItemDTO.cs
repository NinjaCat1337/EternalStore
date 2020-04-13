using System;
using EternalStore.Domain.NotificationManagement;

namespace EternalStore.ApplicationLogic.NotificationManagement.DTO
{
    public class SchedulerItemDTO
    {
        public int IdSchedulerItem { get; set; }
        public string Name { get; set; }
        public string MessageSubject { get; set; }
        public string MessageBody { get; set; }
        public ExecutionFrequency ExecutionFrequency { get; set; }
        public int ExecutionHours { get; set; }
        public int ExecutionMinutes { get; set; }
        public DayOfWeek? ExecutionDayOfWeek { get; set; }
    }
}
