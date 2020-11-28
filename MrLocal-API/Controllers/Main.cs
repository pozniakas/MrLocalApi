using Microsoft.AspNetCore.Mvc;

namespace MrLocal_API.Controllers
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
