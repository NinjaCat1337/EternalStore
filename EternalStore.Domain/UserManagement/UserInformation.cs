using EternalStore.Domain.Models;

namespace EternalStore.Domain.UserManagement
{
    public class UserInformation : Entity
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }

        public virtual User User { get; protected set; }

        protected UserInformation() { }

        internal static UserInformation Insert(string firstName, string lastName, string email) =>
            new UserInformation
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

        internal void Modify(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
