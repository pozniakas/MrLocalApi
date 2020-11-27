using MrLocal_Backend.Models;
using System.Threading.Tasks;

namespace MrLocal_Backend.Services.Interfaces
{
    interface IProductService
    {
        public Task<ProductModel> AddProductToShop(string shopId, string name, string description, string priceType, double? price);
        public Task<ProductModel> UpdateProduct(string id, string shopId, string name, string description, string priceType, double? price);
        public Task<string> DeleteProduct(string id);
    }
}
