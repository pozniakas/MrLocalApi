using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MrLocal_Backend.Controllers.Interfaces
{
    interface IShop
    {
        public Task<Models.Shop> Get(string id);
        public Task<Models.Shop> Post([FromBody] Models.Shop body);
        public Task<Models.Shop> Put([FromBody] Models.Shop body);
        public Task<string> Delete(string id);
    }
}
