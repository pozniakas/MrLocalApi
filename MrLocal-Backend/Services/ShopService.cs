using MrLocal_Backend.LoggerService;
using MrLocal_Backend.Repositories;
using MrLocal_Backend.Services.Helpers;
using MrLocal_Backend.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace MrLocal_Backend.Services
{
    public class ShopService : IShopService
    {
        private readonly ShopRepository shopRepository;
        private readonly Lazy<ValidateData> validateData = null;
        private readonly ILoggerManager _logger;

        public ShopService(ILoggerManager logger)
        {
            validateData = new Lazy<ValidateData>();
            shopRepository = new ShopRepository();
            _logger = logger;
        }

        public async Task<ShopRepository> CreateShop(string name, string description, string typeOfShop, string city)
        {
            _logger.LogInfo("Starting to validate Shop Data");

            var isValidated = await validateData.Value.ValidateShopData(name, null, description, typeOfShop, city, false);

            if (isValidated)
            {
                _logger.LogInfo("Validation completed. Creating shop");
                var createdShop = await shopRepository.Create(name, description, typeOfShop, city);
                _logger.LogInfo("Shop created successfully");
                return createdShop;
            }
            else
            {
                _logger.LogError("Validation failed, invalid shop parameters");

                throw new ArgumentException("Invalid shop parameters for creation");
            }
        }

        public async Task<ShopRepository> UpdateShop(string id, string name, string status, string description, string typeOfShop, string city)
        {
            _logger.LogInfo("Starting to validate Shop Data");

            var isValidated = await validateData.Value.ValidateShopData(name, status, description, typeOfShop, city, true);

            if (isValidated)

            {
                _logger.LogInfo("Validation completed. Updating shop");
                var updatedShop = await shopRepository.Update(id, name, status, description, typeOfShop, city);
                _logger.LogInfo("Shop updated successfully");
                return updatedShop;
            }
            else
            {
                _logger.LogError("Validation failed, invalid shop parameters");

                throw new ArgumentException("Invalid shop parameters for update");
            }
        }

        public async Task<string> DeleteShop(string id)
        {
            _logger.LogInfo($"Finding shop with this id: {id}");

            var shop = await shopRepository.FindOne(id);

            if (shop != null)
            {
                _logger.LogInfo("Shop found, starting to delete");
                var deletedShop = await shopRepository.Delete(id);
                _logger.LogInfo("Shop deleted successfully");
                return deletedShop;
            }
            else
            {
                _logger.LogError($"Shop with id: {id} not found");
                throw new ArgumentException("Invalid id for deleting shop");
            }
        }

        public async Task<ShopRepository> GetShop(string id)
        {
            _logger.LogInfo($"Finding shop with this id : {id}");
            var shop = await shopRepository.FindOne(id);

            if (shop == null)
            {
                _logger.LogError($"Shop with id: {id} not found");
                throw new ArgumentException("Invalid id for getting shop");
            }

            return shop;
        }
    }
}
