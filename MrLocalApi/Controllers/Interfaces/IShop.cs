using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static MrLocalBackend.Models.Body;

namespace MrLocalApi.Controllers.Interfaces
{
    interface IShop
    {
        public Task<IActionResult> Get(string id);
        public Task<IActionResult> Post([FromBody] ShopBody body);
        public Task<IActionResult> Put([FromBody] ShopBody body);
        public Task<IActionResult> Delete(string id);
    }
}
