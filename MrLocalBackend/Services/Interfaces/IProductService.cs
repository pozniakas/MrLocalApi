using MrLocalDb.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrLocalBackend.Services.Interfaces
{
    public interface IProductService
    {
        public Task<Product> AddProductToShop(string shopId, string name, string description, string priceType, decimal? price, string userId);
        public Task<Product> UpdateProduct(string id, string shopId, string name, string description, string priceType, decimal? price, string userId);
        public Task<string> DeleteProduct(string id, string userId);
        public Task<List<Product>> GetAllProducts(string shopId);
    }
}
