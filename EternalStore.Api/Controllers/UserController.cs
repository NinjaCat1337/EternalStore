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
                .RegisterAsync(request.Login, request.Password, request.FirstName, request.LastName, request.Email);

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
            var authResponse = await userManager.LoginAsync(request.Login, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthorizationFailedResponse
                {
                    Error = authResponse.Error
                });
            }

            return Ok(new AuthorizationSuccessResponse
            {
                IdUser = authResponse.IdUser,
                Success = true,
                Token = authResponse.Token,
                Role = authResponse.Role,
                ExpiresInMinutes = authResponse.ExpiresInMinutes
            });
        }

        [HttpGet("{idUser}", Name = "GetUser")]
        public async Task<IActionResult> Get(int idUser)
        {
            var user = await userManager.GetUserAsync(idUser);

            return Ok(user);
        }

        [HttpPut("{idUser}", Name = "ModifyUser")]
        public async Task<IActionResult> Put([FromBody]UserModificationRequest request)
        {
            await userManager.ModifyUserAsync(request.IdUser, request.Login);

            return Ok();
        }

        [HttpPut("{idUser}/information", Name = "ModifyUserInformation")]
        public async Task<IActionResult> Put([FromBody]UserInformationModificationRequest request)
        {
            await userManager.ModifyUserInformationAsync(request.IdUser, request.FirstName, request.LastName, request.Email);

            return Ok();
        }

        [HttpPost("{idUser}/password", Name = "ModifyUserPassword")]
        public async Task<IActionResult> Post([FromBody]UserPasswordModificationRequest request)
        {
            await userManager.ChangePasswordAsync(request.IdUser, request.OldPassword, request.NewPassword);

            return Ok();
        }

        [HttpGet("{idUser}/address", Name = "GetUserAddresses")]
        public async Task<IActionResult> GetUserAddresses(int idUser)
        {
            var userAddresses = await userManager.GetUserAddressesAsync(idUser);

            return Ok(userAddresses);
        }

        [HttpGet("{idUser}/number", Name = "GetUserNumbers")]
        public async Task<IActionResult> GetUserNumbers(int idUser)
        {
            var userNumbers = await userManager.GetUserNumbersAsync(idUser);

            return Ok(userNumbers);
        }

        [HttpPost("{idUser}/address", Name = "AddUserAddress")]
        public async Task<IActionResult> Post([FromBody]UserAddressAdditionRequest request)
        {
            var idUserAddress = await userManager.AddAddressAsync(request.IdUser, request.UserAddress);

            return Ok(idUserAddress);
        }

        [HttpPost("{idUser}/number", Name = "AddUserNumber")]
        public async Task<IActionResult> Post([FromBody]UserNumberAdditionRequest request)
        {
            var idUserNumber = await userManager.AddNumberAsync(request.IdUser, request.UserNumber);

            return Ok(idUserNumber);
        }

        [HttpDelete("{idUser}/address", Name = "RemoveUserAddress")]
        public async Task<IActionResult> DeleteAddress(int idUser, int idUserAddress)
        {
            await userManager.RemoveAddressAsync(idUser, idUserAddress);

            return Ok();
        }

        [HttpDelete("{idUser}/number", Name = "RemoveUserNumber")]
        public async Task<IActionResult> DeleteNumber(int idUser, int idUserNumber)
        {
            await userManager.RemoveNumberAsync(idUser, idUserNumber);

            return Ok();
        }
    }
}
