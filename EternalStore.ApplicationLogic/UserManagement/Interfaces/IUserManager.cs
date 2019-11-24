using EternalStore.ApplicationLogic.UserManagement.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EternalStore.ApplicationLogic.UserManagement.Interfaces
{
    public interface IUserManager : IDisposable
    {
        Task<AuthenticationResult> Register(string login, string password, string firstName, string lastName, string email);
        Task<AuthenticationResult> Login(string login, string password);
        Task AddAddresses(int idUser, IEnumerable<UserAddressDTO> userAddressesDTO);
        Task RemoveAddress(int idUser, int idUserAddress);
        Task AddNumbers(int idUser, IEnumerable<UserNumberDTO> userNumbersDTO);
        Task RemoveNumber(int idUser, int idUserNumber);
        Task Rename(int id, string login);
        Task ChangePassword(int id, string password);
        Task ModifyUserInformation(int id, string firstName, string lastName, string email);
    }
}
