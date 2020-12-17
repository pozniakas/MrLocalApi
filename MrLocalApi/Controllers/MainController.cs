using Microsoft.AspNetCore.Mvc;

namespace MrLocalApi.Controllers
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
