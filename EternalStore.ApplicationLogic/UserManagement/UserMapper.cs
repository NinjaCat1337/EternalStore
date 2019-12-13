using EternalStore.ApplicationLogic.UserManagement.DTO;
using EternalStore.Domain.UserManagement;
using System.Collections.Generic;
using System.Linq;

namespace EternalStore.ApplicationLogic.UserManagement
{
    public static class UserMapper
    {
        public static UserDTO FromUserToUserDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Login = user.Login,
                RegistrationDate = user.RegistrationDate,
                UserInformation = FromUserInformationToUserInformationDTO(user.UserInformation),
                UserAddresses = FromUserAddressesToUserAddressesDTO(user.UserAddresses).ToList(),
                UserNumbers = FromUserNumbersToUserNumbersDTO(user.UserNumbers).ToList()
            };
        }

        public static UserInformationDTO FromUserInformationToUserInformationDTO(UserInformation userInformation) =>
            new UserInformationDTO
            {
                Id = userInformation.Id,
                Email = userInformation.Email,
                FirstName = userInformation.FirstName,
                LastName = userInformation.LastName
            };

        public static IEnumerable<UserAddressDTO> FromUserAddressesToUserAddressesDTO(IEnumerable<UserAddress> userAddresses) =>
            userAddresses.Select(userAddress => new UserAddressDTO
            {
                Id = userAddress.Id,
                UserId = userAddress.UserId,
                Address = userAddress.Address
            });

        public static IEnumerable<UserNumberDTO> FromUserNumbersToUserNumbersDTO(IEnumerable<UserNumber> userNumbers) =>
            userNumbers.Select(userAddress => new UserNumberDTO
            {
                Id = userAddress.Id,
                UserId = userAddress.UserId,
                Number = userAddress.Number
            });
    }
}
