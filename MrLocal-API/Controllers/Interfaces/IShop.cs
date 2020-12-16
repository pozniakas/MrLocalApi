using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MrLocal_API.Controllers.Interfaces
{
    interface IShop
    {
        public Task<IActionResult> Get(string id);
        public Task<IActionResult> Post([FromBody] MrLocal.Backend.Models.Shop body);
        public Task<IActionResult> Put([FromBody] MrLocal.Backend.Models.Shop body);
        public Task<IActionResult> Delete(string id);
    }
}
