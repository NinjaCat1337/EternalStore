using System.Collections.Generic;

namespace EternalStore.ApplicationLogic.UserManagement.DTO
{
    public class RegistrationResult
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
