using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Repositories;
using System.Threading.Tasks;
using static MrLocal_Backend.Models.Body;

namespace MrLocal_Backend.Controllers.Interfaces
{
    interface IShop
    {
        public Task<ShopRepository> Get(string id);
        public Task<ShopRepository> Post([FromBody] ShopBody body);
        public Task<ShopRepository> Put([FromBody] ShopBody body);
        public Task<string> Delete(string id);
    }
}
