using System;
using EternalStore.Domain.Models;

namespace EternalStore.Domain.NotificationManagement
{
    public class SchedulerSettings : Entity
    {
        public ExecutionFrequency ExecutionFrequency { get; protected set; }
        public int ExecutionHours { get; protected set; }
        public int ExecutionMinutes { get; protected set; }
        public DayOfWeek? ExecutionDayOfWeek { get; protected set; }
        public Scheduler Scheduler { get; protected set; }

        protected SchedulerSettings() { }

        public static SchedulerSettings Insert(ExecutionFrequency executionFrequency, int executionHours, int executionMinutes, DayOfWeek? executionDayOfWeek = null)
        {
            Validate(executionFrequency, executionHours, executionMinutes, executionDayOfWeek);

            return new SchedulerSettings
            {
                ExecutionFrequency = executionFrequency,
                ExecutionHours = executionHours,
                ExecutionMinutes = executionMinutes,
                ExecutionDayOfWeek = executionDayOfWeek
            };
        }

        public void Modify(ExecutionFrequency executionFrequency, int executionHours, int executionMinutes, DayOfWeek? executionDayOfWeek = null)
        {
            Validate(executionFrequency, executionHours, executionMinutes, executionDayOfWeek);

            ExecutionFrequency = executionFrequency;
            ExecutionHours = executionHours;
            ExecutionMinutes = executionMinutes;
            ExecutionDayOfWeek = executionDayOfWeek;
        }

        private static void Validate(ExecutionFrequency executionFrequency, int executionHours, int executionMinutes, DayOfWeek? executionDayOfWeek)
        {
            if (executionFrequency == ExecutionFrequency.Weekly && executionDayOfWeek == null)
                throw new Exception("ExecutionDayOfWeek cannot be null if ExecutionFrequency is ExecutionFrequency.Weekly.");

            if (executionHours > 23 || executionHours < 0)
                throw new Exception("Wrong value. Hours value should be from 0 to 23.");

            if (executionMinutes > 59 || executionMinutes < 0)
                throw new Exception("Wrong value. Minutes value should be from 0 to 59.");
        }
    }
}
