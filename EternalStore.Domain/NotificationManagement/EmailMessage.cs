using System;
using EternalStore.Domain.Models;

namespace EternalStore.Domain.NotificationManagement
{
    public class EmailMessage : Entity
    {
        public DateTime SendingDate { get; protected set; }
        public string SenderEmail { get; protected set; }
        public string RecipientEmail { get; protected set; }
        public SchedulerMessage Message { get; protected set; }

        protected EmailMessage() { }

        public static EmailMessage Insert(string sender, string recipient, SchedulerMessage message)
        {
            return new EmailMessage
            {
                SendingDate = DateTime.Now,
                SenderEmail = sender,
                RecipientEmail = recipient,
                Message = message
            };
        }
    }
}
