using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static MrLocalBackend.Models.Body;

namespace MrLocalApi.Controllers.Interfaces
{
    interface IProduct
    {
        public Task<IActionResult> Get(string shopId);
        public Task<IActionResult> Post([FromBody] ProductBody body);
        public Task<IActionResult> Put([FromBody] ProductBody body);
        public Task<IActionResult> Delete(string id);
    }
}
