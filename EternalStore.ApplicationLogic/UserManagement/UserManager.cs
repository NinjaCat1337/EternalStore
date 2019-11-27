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

namespace EternalStore.ApplicationLogic.UserManagement
{
    public class UserManager : IUserManager
    {
        private readonly UserRepository userRepository;
        private readonly string apiKey;

        public UserManager(string connectionString, string apiKey)
        {
            userRepository ??= new UserRepository(connectionString);
            this.apiKey = apiKey;
        }

        public async Task<RegistrationResult> Register(string login, string password, string firstName, string lastName, string email)
        {
            if (userRepository.GetBy(u => u.Login == login).Result.Any())
                return new RegistrationResult
                {
                    Success = false,
                    Errors = new[] { "User with same login already exists." }
                };


            if (password.Length < 6)
                return new RegistrationResult
                {
                    Success = false,
                    Errors = new[] { "Password should contains more than 6 symbols." }
                };

            try
            {
                var user = User.Insert(login, PasswordHashing.GetMd5Hash(password), firstName, lastName, email);

                await userRepository.Insert(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            await userRepository.SaveChangesAsync();

            return new RegistrationResult { Success = true };
        }

        public async Task<AuthenticationResult> Login(string login, string password)
        {
            var findUserByLogin = await userRepository.GetBy(u => u.Login == login);
            var user = findUserByLogin.FirstOrDefault();

            if (user == null)
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = new[] { "User with this login is not exist." }
                };

            if (user.Password != PasswordHashing.GetMd5Hash(password))
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = new[] { "Wrong password." }
                };
            }

            return GenerateAuthenticationResultForUser(login);
        }

        public async Task AddAddresses(int idUser, IEnumerable<UserAddressDTO> userAddressesDTO)
        {
            var user = await userRepository.Get(idUser);

            foreach (var userAddress in userAddressesDTO)
            {
                user.AddAddress(userAddress.Address);
            }

            userRepository.Modify(user);

            await userRepository.SaveChangesAsync();
        }

        public async Task RemoveAddress(int idUser, int idUserAddress)
        {
            var user = await userRepository.Get(idUser);
            userRepository.Eliminate(user.UserAddresses.FirstOrDefault(ua => ua.Id == idUserAddress));

            await userRepository.SaveChangesAsync();
        }

        public async Task AddNumbers(int idUser, IEnumerable<UserNumberDTO> userNumbersDTO)
        {
            var user = await userRepository.Get(idUser);

            foreach (var userNumber in userNumbersDTO)
            {
                user.AddNumber(userNumber.Number);
            }

            userRepository.Modify(user);

            await userRepository.SaveChangesAsync();
        }

        public async Task RemoveNumber(int idUser, int idUserNumber)
        {
            var user = await userRepository.Get(idUser);
            userRepository.Eliminate(user.UserNumbers.FirstOrDefault(un => un.Id == idUserNumber));

            await userRepository.SaveChangesAsync();
        }

        public async Task Rename(int idUser, string login)
        {
            var user = await userRepository.Get(idUser);
            user.Rename(login);
            userRepository.Modify(user);

            await userRepository.SaveChangesAsync();
        }

        public async Task ChangePassword(int idUser, string password)
        {
            var user = await userRepository.Get(idUser);
            user.ModifyPassword(PasswordHashing.GetMd5Hash(password));
            userRepository.Modify(user);

            await userRepository.SaveChangesAsync();
        }

        public async Task ModifyUserInformation(int idUser, string firstName, string lastName, string email)
        {
            var user = await userRepository.Get(idUser);
            user.ModifyUserInformation(firstName, lastName, email);
            userRepository.Modify(user.UserInformation);

            await userRepository.SaveChangesAsync();
        }

        public void Dispose() => userRepository.Dispose();

        #region Private Methods

        private AuthenticationResult GenerateAuthenticationResultForUser(string login)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(apiKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, login),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.Now.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                ExpiresInMinutes = 240
            };
        }

        #endregion
    }
}
