using MrLocal_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrLocal_Backend.Repositories.Interfaces
{
    interface IShopRepository
    {
        public Task<ShopModel> Create(string name, string description, string typeOfShop, string city);
        public Task<ShopModel> Update(string id, string name, string status, string description, string typeOfShop, string city);
        public Task<string> Delete(string id);
        public Task<ShopModel> FindOne(string id);
        public Task<List<ShopModel>> FindAll();
    }
}
