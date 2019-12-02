namespace EternalStore.ApplicationLogic.UserManagement.DTO
{
    public class AuthenticationResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public int ExpiresInMinutes { get; set; }
        public string Error { get; set; }
    }
}
