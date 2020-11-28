using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MrLocal_API.Models.Body;

namespace MrLocal_API.Controllers.Interfaces
{
    interface ISearch
    {
        public Task<IActionResult> Get([FromBody] SearchBody body);
    }
}
