using EternalStore.Domain.Models;
using System;

namespace EternalStore.Domain.UserManagement
{
    public class UserInformation : Entity
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }

        public virtual User User { get; protected set; }

        protected UserInformation() { }

        internal static UserInformation Insert(string firstName, string lastName, string email)
        {
            Validate(firstName, lastName, email);

            return new UserInformation
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
        }

        internal void Modify(string firstName, string lastName, string email)
        {
            Validate(firstName, lastName, email);

            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        private static void Validate(string firstName, string lastName, string email)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new Exception("Name can't be empty.");

            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Email can't be empty.");

            if (firstName.Length > 50 || lastName.Length > 50 || email.Length > 50)
                throw new Exception("First Name/Last Name/Email is too long.");
        }
    }
}
