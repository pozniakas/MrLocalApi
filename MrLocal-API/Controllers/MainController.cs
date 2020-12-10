using Microsoft.AspNetCore.Mvc;

namespace MrLocal_API.Controllers
{
    [ApiController]
    [Route("")]
    public class MainController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Welcome";
        }
    }
}
