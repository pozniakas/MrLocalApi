using MrLocal_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrLocal_Backend.Repositories.Interfaces
{
    interface IProductRepository
    {
        public Task<ProductModel> Create(string shopId, string name, string description, string pricetype, double? price);
        public Task<ProductModel> Update(string id, string shopId, string name, string description, string pricetype, double? price);
        public Task<string> Delete(string id);
        public Task<ProductModel> FindOne(string id);
        public Task<List<ProductModel>> FindAll(string shopId);
    }
}
