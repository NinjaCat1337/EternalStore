using EternalStore.ApplicationLogic.UserManagement.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EternalStore.ApplicationLogic.UserManagement.Interfaces
{
    public interface IUserManager : IDisposable
    {
        Task<RegistrationResult> RegisterAsync(string login, string password, string firstName, string lastName, string email);
        Task<AuthenticationResult> LoginAsync(string login, string password);
        Task<UserDTO> GetUserAsync(int idUser);
        Task<IEnumerable<UserAddressDTO>> GetUserAddressesAsync(int idUser);
        Task<int> AddAddressAsync(int idUser, string address);
        Task RemoveAddressAsync(int idUser, int idUserAddress);
        Task<IEnumerable<UserNumberDTO>> GetUserNumbersAsync(int idUser);
        Task<int> AddNumberAsync(int idUser, string number);
        Task RemoveNumberAsync(int idUser, int idUserNumber);
        Task ModifyUserAsync(int id, string login);
        Task ChangePasswordAsync(int id, string oldPassword, string newPassword);
        Task ModifyUserInformationAsync(int id, string firstName, string lastName, string email);
    }
}
