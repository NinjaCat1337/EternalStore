using EternalStore.ApplicationLogic.UserManagement.DTO;
using EternalStore.ApplicationLogic.UserManagement.Interfaces;
using EternalStore.DataAccess.UserManagement.Repositories;
using EternalStore.Domain.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EternalStore.ApplicationLogic.UserManagement
{
    public class UserManager : IUserManager
    {
        private readonly UserRepository userRepository;

        public UserManager(string connectionString) => userRepository ??= new UserRepository(connectionString);

        private User FindUser(int id)
        {
            var user = userRepository.Get(id);

            if (user == null)
                throw new Exception("User not found.");

            return user;
        }

        public async Task Register(UserDTO userDTO)
        {
            var user = User.Insert(userDTO.Login, PasswordHashing.GetMd5Hash(userDTO.Password), userDTO.UserInformation.FirstName,
                userDTO.UserInformation.LastName, userDTO.UserInformation.Email);

            foreach (var userAddress in user.UserAddresses) { user.AddAddress(userAddress.Address); }

            foreach (var userNumber in user.UserNumbers) { user.AddNumber(userNumber.Number); }

            userRepository.Insert(user);

            await userRepository.SaveChangesAsync();
        }

        public bool Login(UserDTO userDTO)
        {
            var user = userRepository.GetBy(u => u.Login == userDTO.Login).FirstOrDefault();

            if (user == null) return false;

            return user.Password == PasswordHashing.GetMd5Hash(userDTO.Password);
        }

        public async Task AddAddresses(int userId, IEnumerable<UserAddressDTO> userAddressesDTO)
        {
            var user = FindUser(userId);

            foreach (var userAddress in userAddressesDTO) { user.AddAddress(userAddress.Address); }

            userRepository.Modify(user);

            await userRepository.SaveChangesAsync();
        }

        public async Task RemoveAddress(int userId, int userAddressId)
        {
            var user = FindUser(userId);
            user.RemoveAddress(userAddressId);
            userRepository.Modify(user);

            await userRepository.SaveChangesAsync();
        }

        public async Task AddNumbers(int userId, IEnumerable<UserNumberDTO> userNumbersDTO)
        {
            var user = FindUser(userId);

            foreach (var userNumber in userNumbersDTO) { user.AddNumber(userNumber.Number); }

            userRepository.Modify(user);

            await userRepository.SaveChangesAsync();
        }

        public async Task RemoveNumber(int userId, int userNumberId)
        {
            var user = FindUser(userId);
            user.RemoveNumber(userNumberId);
            userRepository.Modify(user);

            await userRepository.SaveChangesAsync();
        }

        public async Task Rename(int id, string login)
        {
            var user = FindUser(id);
            user.Rename(login);
            userRepository.Modify(user);

            await userRepository.SaveChangesAsync();
        }

        public async Task ChangePassword(int id, string password)
        {
            var user = FindUser(id);
            user.ModifyPassword(PasswordHashing.GetMd5Hash(password));
            userRepository.Modify(user);

            await userRepository.SaveChangesAsync();
        }

        public async Task ModifyUserInformation(int id, string firstName, string lastName, string email)
        {
            var user = FindUser(id);
            user.ModifyUserInformation(firstName, lastName, email);
            userRepository.Modify(user);

            await userRepository.SaveChangesAsync();
        }

        public void Dispose() => userRepository.Dispose();
    }
}
