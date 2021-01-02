using MrLocalDb.Entities;
using System.Threading.Tasks;

namespace MrLocalBackend.Services.Interfaces
{
    public interface IShopService
    {
        public Task<Shop> CreateShop(string name, string description, string typeOfShop, string latitude, string longitude, string city, string userId);
        public Task<Shop> UpdateShop(string id, string name, string status, string description, string typeOfShop, string city, Product[] product, string userId);
        public Task<string> DeleteShop(string id, string userId);
        public Task<Shop> GetShop(string id);
    }
}
