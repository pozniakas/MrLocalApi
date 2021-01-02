using MrLocalBackend.Repositories.Interfaces;
using MrLocalBackend.Services.Interfaces;
using MrLocalDb.Entities;
using System;
using System.Threading.Tasks;

namespace MrLocalBackend.Services
{
    public class ShopService : IShopService
    {
        private readonly IShopRepository _shopRepository;
        private readonly Lazy<IValidateData> _validateData = null;
        private readonly ILocationRepository _locationRepository;
        private readonly IUserRepository _userRepository;

        public ShopService(IShopRepository shopRepository, Lazy<IValidateData> validateData, ILocationRepository locationRepository, IUserRepository userRepository)
        {
            _validateData = validateData;
            _shopRepository = shopRepository;
            _locationRepository = locationRepository;
            _userRepository = userRepository;
        }

        public async Task<Shop> CreateShop(string name, string description, string typeOfShop, string latitude, string longitude, string city, string userId)
        {
            var user = await _userRepository.FindOneById(userId);

            if (user != null)
            {
                await _validateData.Value.ValidateShopData(name, null, description, typeOfShop, city, false);
                var createdShop = await _shopRepository.Create(name, description, typeOfShop, city, userId);
                await _locationRepository.Create(latitude, longitude, createdShop.ShopId);
                return createdShop;
            }
            throw new ArgumentException("User doesn't exist");
        }

        public async Task<Shop> UpdateShop(string id, string name, string status, string description, string typeOfShop, string city, Product[] products, string userId)
        {
            var shop = await _shopRepository.FindOne(id);

            if (shop.UserId == userId)
            {
                if (shop == null)
                {
                    throw new ArgumentException("Shop to update doesn't exist");
                }

                foreach (var product in products)
                {
                    await _validateData.Value.ValidateProductData(product.ShopId, product.Name, product.Description, product.Price, false, product.PriceType.ToString());
                }

                await _validateData.Value.ValidateShopData(name, status, description, typeOfShop, city, true);
                var updatedShop = await _shopRepository.Update(id, name, status, description, typeOfShop, city, products);
                return updatedShop;
            }
            throw new ArgumentException("User doesn't have access to update this shop");
        }

        public async Task<string> DeleteShop(string id, string userId)
        {
            var shop = await _shopRepository.FindOne(id);

            if (shop.UserId == userId)
            {
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
            throw new ArgumentException("User doesn't have access to delete this shop");
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
