using MrLocalDb.Entities;
using System.Threading.Tasks;

namespace MrLocalBackend.Services.Interfaces
{
    public interface IShopService
    {
        public Task<Shop> CreateShop(string name, string description, string typeOfShop, string city, string locationId);
        public Task<Shop> UpdateShop(string id, string name, string status, string description, string typeOfShop, string city, Product[] product);
        public Task<string> DeleteShop(string id);
        public Task<Shop> GetShop(string id);
    }
}
