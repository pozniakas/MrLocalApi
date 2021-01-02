using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MrLocalApi.Controllers.Interfaces;
using MrLocalBackend.Authentication.Interfaces;
using System.Threading.Tasks;
using static MrLocalBackend.Models.Body;

namespace MrLocalApi.Controllers
{
    [Route("api/aut")]
    [ApiController]
    [Authorize]
    public class NameController : ControllerBase, IName
    {
        private readonly IJwdAuthenticationManager _JwtAuthenticationManager;

        public NameController(IJwdAuthenticationManager jwtAuthenticationManager)
        {
            _JwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("authentificate")]
        public async Task<IActionResult> Authenticate([FromBody] UserCred userCred)
        {
            var token = _JwtAuthenticationManager.Authenticate(userCred.Username, userCred.Password);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
