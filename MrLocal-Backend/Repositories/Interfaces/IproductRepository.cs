using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrLocal_Backend.Repositories.Interfaces
{
    interface IProductRepository : IRepository
    {
        public void Create(string shopId, string name, string description, string pricetype, double? price);
        public void Update(string id, string shopId, string name, string description, string pricetype, double? price);
        public ProductRepository FindOne(string id);
        public List<ProductRepository> FindAll(string shopId);
    }
}
