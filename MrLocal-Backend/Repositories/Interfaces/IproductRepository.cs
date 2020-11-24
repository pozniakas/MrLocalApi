using System.Collections.Generic;
using System.Threading.Tasks;
using static MrLocal_Backend.Repositories.ProductRepository;

namespace MrLocal_Backend.Repositories.Interfaces
{
    interface IProductRepository : IRepository
    {
        public string ShopId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public PriceTypes PriceType { get; set; }

        public Task<ProductRepository> Create(string shopId, string name, string description, string pricetype, double? price);
        public Task<ProductRepository> Update(string id, string shopId, string name, string description, string pricetype, double? price);
        public Task<ProductRepository> FindOne(string id);
        public Task<List<ProductRepository>> FindAll(string shopId);
    }
}
