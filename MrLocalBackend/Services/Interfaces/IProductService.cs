using MrLocalBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrLocalBackend.Services.Interfaces
{
    public interface IProductService
    {
        public Task<Product> AddProductToShop(string shopId, string name, string description, string priceType, decimal? price);
        public Task<Product> UpdateProduct(string id, string shopId, string name, string description, string priceType, decimal? price);
        public Task<string> DeleteProduct(string id);
        public Task<List<Product>> GetAllProducts(string shopId);
    }
}
