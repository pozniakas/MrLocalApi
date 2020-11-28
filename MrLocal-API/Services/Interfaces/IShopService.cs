using MrLocal_API.Models;
using System.Threading.Tasks;

namespace MrLocal_API.Services.Interfaces
{
    interface IShopService
    {
        public Task<Shop> CreateShop(string name, string description, string typeOfShop, string city);
        public Task<Shop> UpdateShop(string id, string name, string status, string description, string typeOfShop, string city);
        public Task<string> DeleteShop(string id);
        public Task<Shop> GetShop(string id);
    }
}
