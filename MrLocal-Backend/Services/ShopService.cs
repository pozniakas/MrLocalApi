using MrLocal_Backend.Repositories;
using MrLocal_Backend.Services.Helpers;
using MrLocal_Backend.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace MrLocal_Backend.Services
{
    public class ShopService : ValidateData, IShopService
    {
        private readonly ShopRepository shopRepository;

        public ShopService()
        {
            shopRepository = new ShopRepository();
        }

        public async Task<ShopRepository> CreateShop(string name, string description, string typeOfShop, string city)
        {
            if (ValidateShopData(name, null, description, typeOfShop, city, false))
            {
                var createdShop = await shopRepository.Create(name, description, typeOfShop, city);
                return createdShop;
            }
            else
            {
                throw new ArgumentException("Invalid shop parameters for creation");
            }
        }

        public async Task<ShopRepository> UpdateShop(string id, string name, string status, string description, string typeOfShop, string city)
        {
            if (!ValidateShopData(name, status, description, typeOfShop, city, true))
            {
                throw new ArgumentException("Invalid shop parameters for update");
            }

            var updatedShop = await shopRepository.Update(id, name, status, description, typeOfShop, city);
            return updatedShop;
        }

        public void DeleteShop(string id)
        {
            var shop = shopRepository.FindOne(id);

            if (shop != null)
            {
                shopRepository.Delete(id);
            }
            else
            {
                throw new ArgumentException("Invalid id for deleting shop");
            }
        }

        public async Task<ShopRepository> GetShop(string id)
        {
            await Task.Delay(0);

            var shop = shopRepository.FindOne(id);

            if (shop == null)
            {
                throw new ArgumentException("Invalid id for getting shop");
            }

            return shop;
        }
    }
}
