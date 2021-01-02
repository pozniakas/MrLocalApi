using MrLocalBackend.Repositories.Interfaces;
using MrLocalBackend.Services.Interfaces;
using MrLocalDb.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<Product> AddProductToShop(string shopId, string name, string description, string priceType, decimal? price, string userId)
        {
            var shop = await _shopRepository.FindOne(shopId);

            if (shop.UserId == userId)
            {

                await _validateData.Value.ValidateProductData(shopId, name, description, (decimal)price, false, priceType);
                var createdProduct = await _productRepository.Create(shopId, name, description, priceType, (decimal)price);
                return createdProduct;
            }

            throw new ArgumentException("User cannot add products to this shop");
        }

        public async Task<Product> UpdateProduct(string id, string shopId, string name, string description, string priceType, decimal? price, string userId)
        {
            var shop = await _shopRepository.FindOne(shopId);

            if (shop.UserId == userId)
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

            throw new ArgumentException("User cannot add products to this shop");
        }

        public async Task<string> DeleteProduct(string id, string userId)
        {
            var products = await _productRepository.FindOne(id);
            var shop = await _shopRepository.FindOne(products.ShopId);

            if (shop.UserId == userId)
            {

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

            throw new ArgumentException("User cannot delete products of this shop");
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
