using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrLocal_Backend.Repositories.Interfaces
{
    interface IShopRepository : IRepository
    {
        public string Status { get; set; }
        public string Description { get; set; }
        public string TypeOfShop { get; set; }
        public string City { get; set; }

        Task<ShopRepository> Create(string name, string description, string typeOfShop, string city);
        Task<ShopRepository> Update(string id, string name, string status, string description, string typeOfShop, string city);
        public ShopRepository FindOne(string id);
        public List<ShopRepository> FindAll();
    }
}
