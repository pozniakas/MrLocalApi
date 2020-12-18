using System.Collections.Generic;
using System.Threading.Tasks;
using MrLocalDb.Entities;

namespace MrLocalBackend.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public Task<Product> Create(string shopId, string name, string description, string pricetype, decimal price);
        public Task<Product> Update(string id, string shopId, string name, string description, string pricetype, decimal? price);
        public Task<string> Delete(string id);
        public Task<Product> FindOne(string id);
        public Task<List<Product>> FindAll(string shopId);
    }
}
