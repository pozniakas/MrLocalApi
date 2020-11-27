using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Repositories;
using System.Threading.Tasks;
using static MrLocal_Backend.Models.Body;

namespace MrLocal_Backend.Controllers.Interfaces
{
    interface IProduct
    {
        public Task<IActionResult> Post([FromBody] ProductBody body);
        public Task<IActionResult> Put([FromBody] ProductBody body);
        public Task<IActionResult> Delete(string id);
    }
}
