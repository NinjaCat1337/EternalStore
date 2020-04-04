using EternalStore.ApplicationLogic.UserManagement.DTO;
using EternalStore.ApplicationLogic.UserManagement.Interfaces;
using EternalStore.DataAccess.UserManagement.Repositories;
using EternalStore.Domain.UserManagement;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EternalStore.ApplicationLogic.UserManagement
{
    public class UserManager : IUserManager
    {
        private readonly UserRepository userRepository;
        private readonly string apiKey;

        public UserManager(IConfiguration configuration)
        {
            userRepository ??= new UserRepository(configuration.GetConnectionString("DefaultConnection"));
            apiKey = configuration["JwtSettings:ApiKey"];
        }

        public async Task<RegistrationResult> RegisterAsync(string login, string password, string firstName, string lastName, string email)
        {
            if (userRepository.GetByAsync(u => u.Login == login).Result.Any())
                return new RegistrationResult
                {
                    Success = false,
                    Error = "User with same login already exists."
                };

            if (password.Length < 6)
                return new RegistrationResult
                {
                    Success = false,
                    Error = "Password should contains more than 6 symbols."
                };

            var user = User.Insert(login, PasswordHashing.GetMd5Hash(password), firstName, lastName, email, Roles.User);
            await userRepository.InsertAsync(user);

            await userRepository.SaveChangesAsync();

            return new RegistrationResult { Success = true };
        }

        public async Task<AuthenticationResult> LoginAsync(string login, string password)
        {
            var findUserByLogin = await userRepository.GetByAsync(u => u.Login == login);
            var user = findUserByLogin.FirstOrDefault();

            if (user == null)
                return new AuthenticationResult
                {
                    Success = false,
                    Error = "User with this login is not exist."
                };

            if (user.Password != PasswordHashing.GetMd5Hash(password))
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Error = "Wrong password."
                };
            }

            return GenerateAuthenticationResultForUser(user.Id, login, user.Role);
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var allUsers = await userRepository.GetAll().ToListAsync();

            return allUsers.Select(UserMapper.FromUserToUserDTO);
        }

        public async Task<UserDTO> GetUserAsync(int idUser)
        {
            var user = await userRepository.GetAsync(idUser);
            return UserMapper.FromUserToUserDTO(user);
        }

        public async Task<IEnumerable<UserAddressDTO>> GetUserAddressesAsync(int idUser)
        {
            var user = await userRepository.GetAsync(idUser);

            return UserMapper.FromUserAddressesToUserAddressesDTO(user.UserAddresses);
        }

        public async Task<int> AddAddressAsync(int idUser, string userAddress)
        {
            var user = await userRepository.GetAsync(idUser);
            var address = user.AddAddress(userAddress);

            await userRepository.SaveChangesAsync();

            return address.Id;
        }

        public async Task RemoveAddressAsync(int idUser, int idUserAddress)
        {
            var user = await userRepository.GetAsync(idUser);
            userRepository.Eliminate(user.UserAddresses.FirstOrDefault(ua => ua.Id == idUserAddress));

            await userRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserNumberDTO>> GetUserNumbersAsync(int idUser)
        {
            var user = await userRepository.GetAsync(idUser);

            return UserMapper.FromUserNumbersToUserNumbersDTO(user.UserNumbers);
        }

        public async Task<int> AddNumberAsync(int idUser, string userNumber)
        {
            var user = await userRepository.GetAsync(idUser);
            var number = user.AddNumber(userNumber);

            await userRepository.SaveChangesAsync();

            return number.Id;
        }

        public async Task RemoveNumberAsync(int idUser, int idUserNumber)
        {
            var user = await userRepository.GetAsync(idUser);
            userRepository.Eliminate(user.UserNumbers.FirstOrDefault(un => un.Id == idUserNumber));

            await userRepository.SaveChangesAsync();
        }

        public async Task ModifyUserAsync(int idUser, string login)
        {
            var user = await userRepository.GetAsync(idUser);
            user.Modify(login);

            await userRepository.SaveChangesAsync();
        }

        public async Task ChangePasswordAsync(int idUser, string oldPassword, string newPassword)
        {
            var user = await userRepository.GetAsync(idUser);

            if (user.Password != PasswordHashing.GetMd5Hash(oldPassword))
                throw new Exception("Wrong password.");

            user.ModifyPassword(PasswordHashing.GetMd5Hash(newPassword));

            await userRepository.SaveChangesAsync();
        }

        public async Task ModifyUserInformationAsync(int idUser, string firstName, string lastName, string email)
        {
            var user = await userRepository.GetAsync(idUser);
            user.ModifyUserInformation(firstName, lastName, email);

            await userRepository.SaveChangesAsync();
        }

        public void Dispose() => userRepository.Dispose();

        #region Private Methods

        private AuthenticationResult GenerateAuthenticationResultForUser(int idUser, string login, Roles role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(apiKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, login),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, $"{(int)role}")
                }),
                Expires = DateTime.Now.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult
            {
                IdUser = idUser,
                Success = true,
                Token = tokenHandler.WriteToken(token),
                Role = (int)role,
                ExpiresInMinutes = 240
            };
        }

        #endregion
    }
}
