using System.Collections.Generic;
using static MrLocal_Backend.Repositories.ProductRepository;

namespace MrLocal_Backend.Repositories.Interfaces
{
    interface IProductRepository : IRepository
    {
        public string ShopId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public PriceTypes PriceType { get; set; }

        public void Create(string shopId, string name, string description, string pricetype, double? price);
        public void Update(string id, string shopId, string name, string description, string pricetype, double? price);
        public ProductRepository FindOne(string id);
        public List<ProductRepository> FindAll(string shopId);
    }
}
