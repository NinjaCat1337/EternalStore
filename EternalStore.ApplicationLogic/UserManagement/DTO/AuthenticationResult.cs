using System.Collections.Generic;

namespace EternalStore.ApplicationLogic.UserManagement.DTO
{
    public class AuthenticationResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
