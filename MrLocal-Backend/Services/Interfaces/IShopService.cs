using MrLocal_Backend.Repositories;
using System.Threading.Tasks;

namespace MrLocal_Backend.Services.Interfaces
{
    interface IShopService
    {
        Task<ShopRepository> CreateShop(string name, string description, string typeOfShop, string city);
        Task<ShopRepository> UpdateShop(string id, string name, string status, string description, string typeOfShop, string city);
        public Task<string> DeleteShop(string id);
        Task<ShopRepository> GetShop(string id);
    }
}
