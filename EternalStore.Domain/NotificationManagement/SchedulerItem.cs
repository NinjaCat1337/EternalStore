using System;
using EternalStore.Domain.Models;

namespace EternalStore.Domain.NotificationManagement
{
    public class SchedulerItem : Entity
    {
        public string Name { get; protected set; }
        public DateTime ExecutionDateTime { get; protected set; }
        public SchedulerSettings Settings { get; protected set; }
        public SchedulerMessage Message { get; protected set; }

        protected SchedulerItem() { }

        //TODO Validation
        public static SchedulerItem Insert(string name, string messageHeader, string messageBody, ExecutionFrequency executionFrequency, int executionHours, int executionMinutes, DayOfWeek? executionDayOfWeek = null)
        {
            var schedulerMessage = SchedulerMessage.Insert(messageHeader, messageBody);
            var schedulerSettings = SchedulerSettings.Insert(executionFrequency, executionHours, executionMinutes, executionDayOfWeek);

            return new SchedulerItem
            {
                Name = name,
                Settings = schedulerSettings,
                Message = schedulerMessage
            };
        }

        public void Modify(string name) => Name = name;

        public void ModifySchedulerMessage(string subject, string body) => Message.Modify(subject, body);

        /// <summary>
        /// This method sets the execution DateTime depending on the SchedulerSettings.
        /// </summary>
        public void SetExecutionDateTime()
        {
            var currentDate = DateTime.Now;
            ExecutionDateTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day,
                Settings.ExecutionHours, Settings.ExecutionMinutes, 0);

            if (ExecutionDateTime <= currentDate)
                ExecutionDateTime = ExecutionDateTime.AddDays(1);
        }

        /// <summary>
        /// This method updates the date of the next execution depending on the refresh rate in SchedulerSettings.
        /// </summary>
        public void RefreshExecutionDateTime()
        {
            switch (Settings.ExecutionFrequency)
            {
                case ExecutionFrequency.Daily:
                    ExecutionDateTime = ExecutionDateTime.AddDays(1);
                    break;

                case ExecutionFrequency.Weekly:
                    ExecutionDateTime = ExecutionDateTime.AddDays(7);
                    break;
            }
        }
    }
}
