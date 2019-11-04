using EternalStore.Domain.Models;

namespace EternalStore.Domain.UserManagement
{
    public class UserNumber : Entity
    {
        public string Number { get; protected set; }

        public int UserId { get; protected set; }
        public virtual User User { get; protected set; }

        internal static UserNumber Insert(User user, string number) =>
            new UserNumber
            {
                Number = number,
                UserId = user.Id
            };
    }
}
