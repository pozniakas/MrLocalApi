using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MrLocalApi.Controllers.Interfaces
{
    interface IProduct
    {
        public Task<IActionResult> Get(string shopId);
        public Task<IActionResult> Post([FromBody] MrLocalBackend.Models.Product body);
        public Task<IActionResult> Put([FromBody] MrLocalBackend.Models.Product body);
        public Task<IActionResult> Delete(string id);
    }
}
