using System.Collections.Generic;

namespace EternalStore.Api.Contracts.User.Responses
{
    public class RegistrationFailedResponse
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
