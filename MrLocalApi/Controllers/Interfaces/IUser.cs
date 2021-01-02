using Microsoft.AspNetCore.Mvc;
using MrLocalBackend.Models;
using System.Threading.Tasks;

namespace MrLocalApi.Controllers.Interfaces
{
    public interface IUser
    {
        Task<IActionResult> AuthenticateAndLogin([FromBody] Body.UserCred userCred);
        Task<IActionResult> Get();
        Task<IActionResult> Register([FromBody] Body.UserCred userCred);
    }
}