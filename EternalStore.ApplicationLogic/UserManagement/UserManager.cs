using EternalStore.ApplicationLogic.UserManagement.DTO;
using EternalStore.ApplicationLogic.UserManagement.Interfaces;
using EternalStore.DataAccess.UserManagement.Repositories;
using EternalStore.Domain.UserManagement;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EternalStore.ApplicationLogic.UserManagement
{
    public class UserManager : IUserManager
    {
        private readonly UserRepository userRepository;

        public UserManager(string connectionString) => userRepository ??= new UserRepository(connectionString);

        public async Task Register(UserDTO userDTO)
        {
            var user = User.Insert(userDTO.Login, PasswordHashing.GetMd5Hash(userDTO.Password), userDTO.UserInformation.FirstName,
                userDTO.UserInformation.LastName, userDTO.UserInformation.Email);

            foreach (var userAddress in user.UserAddresses)
            {
                user.AddAddress(userAddress.Address);
            }

            foreach (var userNumber in user.UserNumbers)
            {
                user.AddNumber(userNumber.Number);
            }

            userRepository.Insert(user);

            await userRepository.SaveChangesAsync();
        }

        public bool Login(UserDTO userDTO)
        {
            var user = userRepository.GetBy(u => u.Login == userDTO.Login).FirstOrDefault();

            if (user == null) return false;

            return user.Password == PasswordHashing.GetMd5Hash(userDTO.Password);
        }

        public async Task AddAddresses(int idUser, IEnumerable<UserAddressDTO> userAddressesDTO)
        {
            var user = userRepository.Get(idUser);

            foreach (var userAddress in userAddressesDTO)
            {
                user.AddAddress(userAddress.Address);
            }

            userRepository.Modify(user);

            await userRepository.SaveChangesAsync();
        }

        public async Task RemoveAddress(int idUser, int idUserAddress)
        {
            userRepository.Eliminate(userRepository.Get(idUser).UserAddresses.FirstOrDefault(ua => ua.Id == idUserAddress));

            await userRepository.SaveChangesAsync();
        }

        public async Task AddNumbers(int idUser, IEnumerable<UserNumberDTO> userNumbersDTO)
        {
            var user = userRepository.Get(idUser);

            foreach (var userNumber in userNumbersDTO)
            {
                user.AddNumber(userNumber.Number);
            }

            userRepository.Modify(user);

            await userRepository.SaveChangesAsync();
        }

        public async Task RemoveNumber(int idUser, int idUserNumber)
        {
            userRepository.Eliminate(userRepository.Get(idUser).UserNumbers.FirstOrDefault(un => un.Id == idUserNumber));

            await userRepository.SaveChangesAsync();
        }

        public async Task Rename(int idUser, string login)
        {
            var user = userRepository.Get(idUser);
            user.Rename(login);
            userRepository.Modify(user);

            await userRepository.SaveChangesAsync();
        }

        public async Task ChangePassword(int idUser, string password)
        {
            var user = userRepository.Get(idUser);
            user.ModifyPassword(PasswordHashing.GetMd5Hash(password));
            userRepository.Modify(user);

            await userRepository.SaveChangesAsync();
        }

        public async Task ModifyUserInformation(int idUser, string firstName, string lastName, string email)
        {
            var user = userRepository.Get(idUser);
            user.ModifyUserInformation(firstName, lastName, email);
            userRepository.Modify(user.UserInformation);

            await userRepository.SaveChangesAsync();
        }

        public void Dispose() => userRepository.Dispose();
    }
}
