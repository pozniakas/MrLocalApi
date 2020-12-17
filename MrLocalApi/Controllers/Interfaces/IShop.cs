using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MrLocalApi.Controllers.Interfaces
{
    interface IShop
    {
        public Task<IActionResult> Get(string id);
        public Task<IActionResult> Post([FromBody] MrLocalBackend.Models.Shop body);
        public Task<IActionResult> Put([FromBody] MrLocalBackend.Models.Shop body);
        public Task<IActionResult> Delete(string id);
    }
}
