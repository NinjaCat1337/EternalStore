using EternalStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EternalStore.Domain.UserManagement
{
    public class User : Entity
    {
        public string Login { get; protected set; }
        public string Password { get; protected set; }
        public DateTime RegistrationDate { get; protected set; }
        public UserInformation UserInformation { get; protected set; }

        public IEnumerable<UserNumber> UserNumbers => userNumbers?.AsEnumerable();
        private readonly List<UserNumber> userNumbers = new List<UserNumber>();

        public IEnumerable<UserAddress> UserAddresses => userAddresses?.AsEnumerable();
        private readonly List<UserAddress> userAddresses = new List<UserAddress>();

        protected User() { }

        public static User Insert(string login, string password, string firstName, string lastName, string email) =>
            new User
            {
                Login = login,
                Password = password,
                RegistrationDate = DateTime.Now,
                UserInformation = UserInformation.Insert(firstName, lastName, email)
            };

        public void Rename(string login) => Login = login;

        public void ModifyPassword(string password) => Password = password;

        public void ModifyUserInformation(string firstName, string lastName, string email) =>
            UserInformation.Modify(firstName, lastName, email);

        public void AddAddress(string address) => userAddresses.Add(UserAddress.Insert(this, address));

        public void RemoveAddress(int userAddressId)
        {
            var userAddress = userAddresses.FirstOrDefault(ua => ua.Id == userAddressId);

            if (userAddress == null) throw new Exception("User Address not found.");

            userAddresses.Remove(userAddresses.FirstOrDefault(ua => ua.Id == userAddressId));
        }

        public void AddNumber(string number) => userNumbers.Add(UserNumber.Insert(this, number));

        public void RemoveNumber(int userNumberId)
        {
            var userNumber = userNumbers.FirstOrDefault(un => un.Id == userNumberId);

            if (userNumber == null) throw new Exception("User Number not found.");

            userNumbers.Remove(userNumber);
        }
    }
}
