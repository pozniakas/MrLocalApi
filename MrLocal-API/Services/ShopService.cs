using MrLocal_API.Models;
using MrLocal_API.Repositories.Interfaces;
using MrLocal_API.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace MrLocal_API.Services
{
    public class ShopService : IShopService
    {
        private readonly IShopRepository _shopRepository;
        private readonly Lazy<IValidateData> _validateData = null;

        public ShopService(IShopRepository shopRepository, Lazy<IValidateData> validateData)
        {
            _validateData = validateData;
            _shopRepository = shopRepository;
        }

        public async Task<Shop> CreateShop(string name, string description, string typeOfShop, string city)
        {
            await _validateData.Value.ValidateShopData(name, null, description, typeOfShop, city, false);
            var createdShop = await _shopRepository.Create(name, description, typeOfShop, city);
            return createdShop;
        }

        public async Task<Shop> UpdateShop(string id, string name, string status, string description, string typeOfShop, string city)
        {
            var shop = await _shopRepository.FindOne(id);

            if (shop == null)
            {
                throw new ArgumentException("Shop to update doesn't exist");
            }

            await _validateData.Value.ValidateShopData(name, status, description, typeOfShop, city, true);
            var updatedShop = await _shopRepository.Update(id, name, status, description, typeOfShop, city);
            return updatedShop;
        }

        public async Task<string> DeleteShop(string id)
        {
            var shop = await _shopRepository.FindOne(id);

            if (shop != null)
            {
                var deletedShop = await _shopRepository.Delete(id);
                return deletedShop;
            }
            else
            {
                throw new ArgumentException("Invalid id for deleting shop");
            }
        }

        public async Task<Shop> GetShop(string id)
        {
            var shop = await _shopRepository.FindOne(id);

            if (shop == null)
            {
                throw new ArgumentException("Invalid id for getting shop");
            }

            return shop;
        }
    }
}
