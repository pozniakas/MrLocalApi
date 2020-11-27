using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Models;
using System.Threading.Tasks;

namespace MrLocal_Backend.Controllers.Interfaces
{
    interface IShop
    {
        public Task<ShopModel> Get(string id);
        public Task<ShopModel> Post([FromBody] ShopModel body);
        public Task<ShopModel> Put([FromBody] ShopModel body);
        public Task<string> Delete(string id);
    }
}
