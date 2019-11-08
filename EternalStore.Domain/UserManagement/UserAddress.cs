using EternalStore.Domain.Models;
using System;

namespace EternalStore.Domain.UserManagement
{
    public class UserAddress : Entity
    {
        public string Address { get; protected set; }

        public int UserId { get; protected set; }
        public virtual User User { get; protected set; }

        protected UserAddress() { }

        internal static UserAddress Insert(User user, string address)
        {
            Validate(address);

            return new UserAddress
            {
                Address = address,
                UserId = user.Id
            };
        }

        private static void Validate(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new Exception("You can't add empty address.");

            if (address.Length > 100)
                throw new Exception("Address is too long.");
        }
    }
}
