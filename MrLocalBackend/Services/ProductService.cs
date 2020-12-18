using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MrLocalBackend.Repositories.Interfaces;
using MrLocalBackend.Services.Interfaces;
using MrLocalDb.Entities;

namespace MrLocalBackend.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IShopRepository _shopRepository;
        private readonly Lazy<IValidateData> _validateData = null;

        public ProductService(IProductRepository productRepository, IShopRepository shopRepository, Lazy<IValidateData> validateData)
        {
            _productRepository = productRepository;
            _shopRepository = shopRepository;
            _validateData = validateData;
        }

        public async Task<Product> AddProductToShop(string shopId, string name, string description, string priceType, decimal? price)
        {
            await _validateData.Value.ValidateProductData(shopId, name, description, (decimal)price, false, priceType);
            var createdProduct = await _productRepository.Create(shopId, name, description, priceType, (decimal)price);
            return createdProduct;
        }

        public async Task<Product> UpdateProduct(string id, string shopId, string name, string description, string priceType, decimal? price)
        {
            var product = await _productRepository.FindOne(id);

            if (product == null)
            {
                throw new ArgumentException("Product to update doesn't exist");
            }

            await _validateData.Value.ValidateProductData(shopId, name, description, price, true, priceType);
            var updatedProduct = await _productRepository.Update(id, shopId, name, description, priceType, price);
            return updatedProduct;
        }

        public async Task<string> DeleteProduct(string id)
        {
            var products = await _productRepository.FindOne(id);

            if (products != null)
            {
                var deletedProduct = await _productRepository.Delete(id);
                return deletedProduct;
            }
            else
            {
                throw new ArgumentException("Invalid products parameters for deleting");
            }
        }

        public async Task<List<Product>> GetAllProducts(string shopId)
        {
            var shop = await _shopRepository.FindOne(shopId);

            if (shop == null)
            {
                throw new ArgumentException("Couldn't get products of shop which doesn't exist");
            }

            var products = await _productRepository.FindAll(shopId);
            return products;
        }
    }
}
