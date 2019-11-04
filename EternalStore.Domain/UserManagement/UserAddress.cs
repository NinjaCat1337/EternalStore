using EternalStore.Domain.Models;

namespace EternalStore.Domain.UserManagement
{
    public class UserAddress : Entity
    {
        public string Address { get; protected set; }

        public int UserId { get; protected set; }
        public virtual User User { get; protected set; }

        protected UserAddress() { }

        internal static UserAddress Insert(User user, string address) =>
            new UserAddress
            {
                Address = address,
                UserId = user.Id
            };
    }
}
