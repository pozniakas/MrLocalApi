using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static MrLocalBackend.Models.Body;

namespace MrLocalApi.Controllers.Interfaces
{
    public interface ILogin
    {
        public Task<IActionResult> Put([FromBody] UserCred userCred);
    }
}