using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MrLocal_Backend.Controllers.Interfaces
{
    interface IShop
    {
        public Task<IActionResult> Get(string id);
        public Task<IActionResult> Post([FromBody] Models.Shop body);
        public Task<IActionResult> Put([FromBody] Models.Shop body);
        public Task<IActionResult> Delete(string id);
    }
}
