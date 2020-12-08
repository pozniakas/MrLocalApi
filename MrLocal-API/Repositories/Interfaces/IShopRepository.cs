using MrLocal_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrLocal_API.Repositories.Interfaces
{
    public interface IShopRepository
    {
        public Task<Shop> Create(string name, string description, string typeOfShop, string city);
        public Task<Shop> Update(string id, string name, string status, string description, string typeOfShop, string city);
        public Task<string> Delete(string id);
        public Task<Shop> FindOne(string id);
        public Task<List<Shop>> FindAll();
    }
}
