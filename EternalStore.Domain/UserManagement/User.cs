﻿using EternalStore.Domain.Models;
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
        public Roles Role { get; protected set; }

        public IEnumerable<UserNumber> UserNumbers => userNumbers?.AsEnumerable();
        private readonly List<UserNumber> userNumbers = new List<UserNumber>();

        public IEnumerable<UserAddress> UserAddresses => userAddresses?.AsEnumerable();
        private readonly List<UserAddress> userAddresses = new List<UserAddress>();

        protected User() { }

        public static User Insert(string login, string password, string firstName, string lastName, string email, Roles role)
        {
            Validate(login);

            return new User
            {
                Login = login,
                Password = password,
                RegistrationDate = DateTime.Now,
                UserInformation = UserInformation.Insert(firstName, lastName, email),
                Role = role
            };
        }

        public void Modify(string login)
        {
            Validate(login);

            Login = login;
        }

        public void ModifyPassword(string password) => Password = password;

        public void ModifyUserInformation(string firstName, string lastName, string email) =>
            UserInformation.Modify(firstName, lastName, email);

        public UserAddress AddAddress(string address)
        {
            var userAddress = UserAddress.Insert(this, address);
            userAddresses.Add(userAddress);

            return userAddress;
        }

        public UserNumber AddNumber(string number)
        {
            var userNumber = UserNumber.Insert(this, number);
            userNumbers.Add(userNumber);

            return userNumber;
        }

        private static void Validate(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new Exception("Login can't be empty.");

            if (login.Length > 50)
                throw new Exception("Login is too long.");
        }
    }
}
