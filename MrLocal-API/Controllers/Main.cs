using Microsoft.AspNetCore.Mvc;

namespace MrLocal_Backend.Controllers
{
    [ApiController]
    [Route("")]
    public class Main : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Welcome";
        }
    }
}
