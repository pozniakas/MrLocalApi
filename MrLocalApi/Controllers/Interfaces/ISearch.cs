using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static MrLocalBackend.Models.Body;

namespace MrLocalApi.Controllers.Interfaces
{
    interface ISearch
    {
        public Task<IActionResult> Get([FromBody] SearchBody body);
    }
}
