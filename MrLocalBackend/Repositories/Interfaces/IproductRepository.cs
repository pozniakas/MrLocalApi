using MrLocalBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrLocalBackend.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public Task<Product> Create(string shopId, string name, string description, string pricetype, double? price);
        public Task<Product> Update(string id, string shopId, string name, string description, string pricetype, double? price);
        public Task<string> Delete(string id);
        public Task<Product> FindOne(string id);
        public Task<List<Product>> FindAll(string shopId);
    }
}
