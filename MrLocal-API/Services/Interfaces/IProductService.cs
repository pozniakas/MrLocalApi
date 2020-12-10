using MrLocal_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrLocal_API.Services.Interfaces
{
    public interface IProductService
    {
        public Task<Product> AddProductToShop(string shopId, string name, string description, string priceType, double? price);
        public Task<Product> UpdateProduct(string id, string shopId, string name, string description, string priceType, double? price);
        public Task<string> DeleteProduct(string id);
        public Task<List<Product>> GetAllProducts(string shopId);
    }
}
