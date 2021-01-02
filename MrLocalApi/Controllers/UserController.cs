using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MrLocalApi.Controllers.Interfaces;
using MrLocalBackend.Authentication.Interfaces;
using MrLocalBackend.Services.Interfaces;
using System.Threading.Tasks;
using static MrLocalBackend.Models.Body;

namespace MrLocalApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : Controller, IUser
    {
        private readonly IJwdAuthenticationManager _jwtAuthenticationManager;
        private readonly IUserService _userService;

        public UserController(IJwdAuthenticationManager jwtAuthenticationManager, IUserService userService)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCred userCred)
        {
            var user = await _userService.CreateUser(userCred.Username, userCred.Password);
            return ReturnResponse(user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateAndLogin([FromBody] UserCred userCred)
        {
            var token = await _jwtAuthenticationManager.AuthenticateAsync(userCred.Username, userCred.Password);

            if (token == null)
            {
                return Unauthorized();
            }

            return ReturnResponse(token);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = await _userService.GetUserById(HttpContext.User.Identity.Name);
            return ReturnResponse(user);
        }

        public IActionResult ReturnResponse(object value) => Ok(new { Response = value });
    }
}
