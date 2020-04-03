using System.Threading.Tasks;
using EternalStore.Domain.NotificationManagement;

namespace EternalStore.ApplicationLogic.NotificationManagement.Interfaces
{
    public interface IMailManager
    {
        Task SendEmailAsync(string userName, string email, SchedulerMessage message);
    }
}
