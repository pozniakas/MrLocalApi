using Microsoft.AspNetCore.Mvc;
using MrLocalApi.Controllers.Interfaces;
using System.Threading.Tasks;
using static MrLocalBackend.Models.Body;

namespace MrLocalApi.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : ControllerBase, IRegister
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserCred userCred)
        {
            return Ok();
        }
    }
}
