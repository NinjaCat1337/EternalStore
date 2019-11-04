using EternalStore.ApplicationLogic.UserManagement.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EternalStore.ApplicationLogic.UserManagement.Interfaces
{
    public interface IUserManager : IDisposable
    {
        Task Register(UserDTO userDTO);
        bool Login(UserDTO userDTO);
        Task AddAddresses(int userId, IEnumerable<UserAddressDTO> userAddressesDTO);
        Task RemoveAddress(int userId, int userAddressId);
        Task AddNumbers(int userId, IEnumerable<UserNumberDTO> userNumbersDTO);
        Task RemoveNumber(int userId, int userNumberId);
        Task Rename(int id, string login);
        Task ChangePassword(int id, string password);
        Task ModifyUserInformation(int id, string firstName, string lastName, string email);
    }
}
