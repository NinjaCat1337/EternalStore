namespace EternalStore.Api.Contracts.User.Responses
{
    public class AuthorizationSuccessResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public int ExpiresInMinutes { get; set; }
    }
}
