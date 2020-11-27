using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Models;
using System.Threading.Tasks;
using static MrLocal_Backend.Models.Body;

namespace MrLocal_Backend.Controllers.Interfaces
{
    interface IShop
    {
        public Task<ShopModel> Get(string id);
        public Task<ShopModel> Post([FromBody] ShopBody body);
        public Task<ShopModel> Put([FromBody] ShopBody body);
        public Task<string> Delete(string id);
    }
}
