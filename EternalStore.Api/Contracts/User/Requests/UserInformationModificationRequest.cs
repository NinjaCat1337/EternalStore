namespace EternalStore.Api.Contracts.User.Requests
{
    public class UserInformationModificationRequest
    {
        public int IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
