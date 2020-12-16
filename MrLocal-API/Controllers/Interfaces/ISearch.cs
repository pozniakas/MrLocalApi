using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static MrLocal.Backend.Models.Body;

namespace MrLocal_API.Controllers.Interfaces
{
    interface ISearch
    {
        public Task<IActionResult> Get([FromBody] SearchBody body);
    }
}
