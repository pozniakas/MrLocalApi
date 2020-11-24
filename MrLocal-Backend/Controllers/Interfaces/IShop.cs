using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Repositories;
using System.Threading.Tasks;
using static MrLocal_Backend.Models.Body;

namespace MrLocal_Backend.Controllers.Interfaces
{
    interface IShop
    {
        Task<ShopRepository> Get(string id);
        Task<ShopRepository> Post([FromBody] ShopBody body);
        Task<ShopRepository> Put([FromBody] ShopBody body);
        Task<string> Delete(string id);
    }
}
