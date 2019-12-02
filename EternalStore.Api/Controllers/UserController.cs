using EternalStore.Api.Contracts.User.Requests;
using EternalStore.Api.Contracts.User.Responses;
using EternalStore.ApplicationLogic.UserManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EternalStore.Api.Controllers
{
    [Route("api/[controller]/")]
    public class UserController : Controller
    {
        private readonly IUserManager userManager;
        public UserController(IUserManager userManager) => this.userManager = userManager;

        // GET: api/<controller>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegistrationRequest request)
        {
            var registrationResponse = await userManager
                .Register(request.Login, request.Password, request.FirstName, request.LastName, request.Email);

            if (!registrationResponse.Success)
            {
                return BadRequest(new RegistrationFailedResponse
                {
                    Success = false,
                    Error = registrationResponse.Error
                });
            }

            return Ok(new RegistrationSuccessResponse
            {
                Success = true
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserLoginRequest request)
        {
            var authResponse = await userManager.Login(request.Login, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthorizationFailedResponse
                {
                    Error = authResponse.Error
                });
            }

            return Ok(new AuthorizationSuccessResponse
            {
                Success = true,
                Token = authResponse.Token,
                ExpiresInMinutes = authResponse.ExpiresInMinutes
            });
        }
    }
}
