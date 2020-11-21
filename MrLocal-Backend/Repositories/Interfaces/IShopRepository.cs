using System.Collections.Generic;

namespace MrLocal_Backend.Repositories.Interfaces
{
    interface IShopRepository : IRepository
    {
        public void Create(string name, string description, string typeOfShop, string city);
        public void Update(string id, string name, string status, string description, string typeOfShop, string city);
        public ShopRepository FindOne(string id);
        public List<ShopRepository> FindAll();
    }
}
