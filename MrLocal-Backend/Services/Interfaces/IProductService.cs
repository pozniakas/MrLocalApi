using MrLocal_Backend.Models;
using System.Threading.Tasks;

namespace MrLocal_Backend.Services.Interfaces
{
    interface IProductService
    {
        public Task<Product> AddProductToShop(string shopId, string name, string description, string priceType, double? price);
        public Task<Product> UpdateProduct(string id, string shopId, string name, string description, string priceType, double? price);
        public Task<string> DeleteProduct(string id);
    }
}
