using System;
using Microsoft.AspNetCore.Mvc;

namespace MrLocal_Backend.Controllers
{
    [ApiController]
    [Route("")]
    public class Main : ApiController
    {
        [HttpGet]
        public string Get()
        {
            return "Welcome";
        }
    }
}
