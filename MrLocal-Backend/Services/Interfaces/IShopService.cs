using MrLocal_Backend.Repositories;

namespace MrLocal_Backend.Services.Interfaces
{
    interface IShopService
    {
        public void CreateShop(string name, string description, string typeOfShop, string city);
        public void UpdateShop(string id, string name, string status, string description, string typeOfShop, string city);
        public void DeleteShop(string id);
        public ShopRepository GetShop(string id);
    }
}
