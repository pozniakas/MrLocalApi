using MrLocal_Backend.Repositories;
using System.Threading.Tasks;

namespace MrLocal_Backend.Services.Interfaces
{
    interface IProductService
    {
        Task<ProductRepository> AddProductToShop(string shopId, string name, string description, string priceType, double? price);
        Task<ProductRepository> UpdateProduct(string id, string shopId, string name, string description, string priceType, double? price);
        public Task<string> DeleteProduct(string id);
    }
}
