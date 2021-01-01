using Microsoft.AspNetCore.Mvc;
using MrLocalApi.Controllers.Interfaces;
using System.Threading.Tasks;
using static MrLocalBackend.Models.Body;

namespace MrLocalApi.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase, ILogin
    {
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserCred userCred)
        {
            return Ok();
        }
    }
}
