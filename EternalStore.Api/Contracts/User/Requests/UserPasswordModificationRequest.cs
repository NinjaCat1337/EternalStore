namespace EternalStore.Api.Contracts.User.Requests
{
    public class UserPasswordModificationRequest
    {
        public int IdUser { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
