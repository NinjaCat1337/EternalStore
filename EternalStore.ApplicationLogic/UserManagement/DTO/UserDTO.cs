using System;
using System.Collections.Generic;

namespace EternalStore.ApplicationLogic.UserManagement.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public UserInformationDTO UserInformation { get; set; }

        public List<UserNumberDTO> UserNumbers { get; set; }

        public List<UserAddressDTO> UserAddresses { get; set; }
    }
}
