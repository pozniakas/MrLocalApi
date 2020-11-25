using MrLocal_Backend.Repositories;
using System.Threading.Tasks;

namespace MrLocal_Backend.Services.Interfaces
{
    interface IShopService
    {
        public Task<ShopRepository> CreateShop(string name, string description, string typeOfShop, string city);
        public Task<ShopRepository> UpdateShop(string id, string name, string status, string description, string typeOfShop, string city);
        public Task<string> DeleteShop(string id);
        public Task<ShopRepository> GetShop(string id);
    }
}
