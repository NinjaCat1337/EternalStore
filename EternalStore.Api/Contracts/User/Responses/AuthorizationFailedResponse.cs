using System.Collections.Generic;

namespace EternalStore.Api.Contracts.User.Responses
{
    public class AuthorizationFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
