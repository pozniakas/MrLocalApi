using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static MrLocalBackend.Models.Body;

namespace MrLocalApi.Controllers.Interfaces
{
    public interface IRegister
    {
        public Task<IActionResult> Post([FromBody] UserCred userCred);
    }
}