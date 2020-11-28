using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MrLocal_Backend.Controllers.Interfaces
{
    interface IProduct
    {
        public Task<IActionResult> Get(string shopId);
        public Task<IActionResult> Post([FromBody] Models.Product body);
        public Task<IActionResult> Put([FromBody] Models.Product body);
        public Task<IActionResult> Delete(string id);
    }
}
