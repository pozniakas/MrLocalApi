using MrLocal_Backend.Models;
using System.Threading.Tasks;

namespace MrLocal_Backend.Services.Interfaces
{
    interface IShopService
    {
        public Task<ShopModel> CreateShop(string name, string description, string typeOfShop, string city);
        public Task<ShopModel> UpdateShop(string id, string name, string status, string description, string typeOfShop, string city);
        public Task<string> DeleteShop(string id);
        public Task<ShopModel> GetShop(string id);
    }
}
