namespace EternalStore.Api.Contracts.User.Responses
{
    public class AuthorizationSuccessResponse
    {
        public int IdUser { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public int Role { get; set; }
        public int ExpiresInMinutes { get; set; }
    }
}
