using System;
using System.Collections.Generic;
using EternalStore.Domain.Models;

namespace EternalStore.Domain.NotificationManagement
{
    public class SchedulerMessage : Entity
    {
        public string Subject { get; protected set; }
        public string Body { get; protected set; }
        public SchedulerItem SchedulerItem { get; protected set; }

        public IEnumerable<EmailMessage> EmailMessages { get; protected set; }

        protected SchedulerMessage() { }

        public static SchedulerMessage Insert(string header, string body)
        {
            Validate(header, body);

            return new SchedulerMessage
            {
                Subject = header,
                Body = body
            };
        }

        public void Modify(string header, string body)
        {
            Validate(header, body);

            Subject = header;
            Body = body;
        }

        private static void Validate(string header, string body)
        {
            if (string.IsNullOrWhiteSpace(header) || string.IsNullOrWhiteSpace(body))
                throw new Exception("Header or body can't be empty.");

            if (header.Length > 100)
                throw new Exception("Header is too long.");

            if (body.Length > 5000)
                throw new Exception("Body is too long.");
        }
    }
}
