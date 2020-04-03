using System;
using System.Threading.Tasks;
using EternalStore.ApplicationLogic.NotificationManagement.Interfaces;
using EternalStore.DataAccess.NotificationManagement.Repositories;
using EternalStore.Domain.NotificationManagement;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace EternalStore.ApplicationLogic.NotificationManagement
{
    public class MailManager : IMailManager
    {
        private readonly EmailMessageRepository emailMessageRepository;

        private readonly string smtpAddress;
        private readonly int smtpPort;
        private readonly bool useSsl;
        private readonly string smtpLogin;
        private readonly string smtpPassword;

        public MailManager(IConfiguration configuration)
        {
            emailMessageRepository ??= new EmailMessageRepository(configuration.GetConnectionString("DefaultConnection"));

            smtpAddress = configuration["SmtpSettings:smtpAddress"];
            smtpPort = Convert.ToInt32(configuration["SmtpSettings:smtpPort"]);
            useSsl = Convert.ToBoolean(configuration["SmtpSettings:useSsl"]);
            smtpLogin = configuration["SmtpSettings:smtpLogin"];
            smtpPassword = configuration["SmtpSettings:smtpPassword"];
        }

        public async Task SendEmailAsync(string userName, string email, SchedulerMessage message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Eternal Store", smtpLogin));
            emailMessage.To.Add(new MailboxAddress(userName, email));
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message.Body
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(smtpAddress, smtpPort, useSsl);
            await client.AuthenticateAsync(smtpLogin, smtpPassword);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);

            var dbEmailMessage = EmailMessage.Insert(smtpLogin, email, message);
            await emailMessageRepository.InsertAsync(dbEmailMessage);
        }
    }
}
