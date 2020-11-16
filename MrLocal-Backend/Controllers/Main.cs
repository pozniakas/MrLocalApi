using System;
using Microsoft.AspNetCore.Mvc;

namespace MrLocal_Backend.Controllers
{
    [ApiController]
    [Route("")]
    public class Main : Arguments
    {
        [HttpGet]
        public string Get()
        {
            return "Welcome";
        }
    }
}
