using MrLocal_Backend.LoggerService;
using MrLocal_Backend.Repositories;
using MrLocal_Backend.Services.Helpers;
using MrLocal_Backend.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace MrLocal_Backend.Services
{
    class ProductService : IProductService
    {
        private readonly ProductRepository productRepository;
        private readonly Lazy<ValidateData> validateData = null;
        private readonly ILoggerManager _logger;

        public ProductService(ILoggerManager logger)
        {
            _logger = logger;
            productRepository = new ProductRepository();
            validateData = new Lazy<ValidateData>();
        }

        public async Task<ProductRepository> AddProductToShop(string shopId, string name, string description, string priceType, double? price)
        {
            _logger.LogInfo("Validating product data");
            if (validateData.Value.ValidateProductData(shopId, name, description, price, false, priceType))
            {
                _logger.LogInfo("Validation completed, creating product");
                var createdProduct = await productRepository.Create(shopId, name, description, priceType, price);
                _logger.LogInfo("Product created succssesfully");
                return createdProduct;
            }
            else
            {
                _logger.LogError("Validation failed: Invalid product parameters");

                throw new ArgumentException("Invalid products parameters for creation");
            }
        }

        public async Task<ProductRepository> UpdateProduct(string id, string shopId, string name, string description, string priceType, double? price)
        {
            _logger.LogInfo("Validating product data");
            if (validateData.Value.ValidateProductData(shopId, name, description, price, true, priceType, id))
            {
                _logger.LogInfo("Validation completed, updating product");
                var updatedProduct = await productRepository.Update(id, shopId, name, description, priceType, price);
                _logger.LogInfo("Product updated succssesfully");
                return updatedProduct;
            }
            else
            {
                _logger.LogError("Validation failed: Invalid product parameters");

                throw new ArgumentException("Invalid products parameters for creation");
            }
        }

        public async Task<string> DeleteProduct(string id)
        {
            _logger.LogInfo($"Finding product with id: {id}");

            var products = await productRepository.FindOne(id);

            if (products != null)
            {
                _logger.LogInfo("Product found, deleteing product");
                var deletedProduct = await productRepository.Delete(id);
                _logger.LogInfo("Product deleted succssesfully");
                return deletedProduct;
            }
            else
            {
                _logger.LogError($"Product with id: {id} not found");
                throw new ArgumentException("Invalid products parameters for creation");
            }
        }
    }
}
