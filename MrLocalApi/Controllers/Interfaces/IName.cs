using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static MrLocalBackend.Models.Body;

namespace MrLocalApi.Controllers.Interfaces
{
    interface IName
    {
        public Task<IActionResult> Get(int id);
        public Task<IActionResult> Authenticate([FromBody] UserCred userCred);
    }
}
