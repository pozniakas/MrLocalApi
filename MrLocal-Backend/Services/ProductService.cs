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

        public ProductService()
        {
            validateData = new Lazy<ValidateData>();
            productRepository = new ProductRepository();
        }

        public async Task<ProductRepository> AddProductToShop(string shopId, string name, string description, string priceType, double? price)
        {
            if (validateData.Value.ValidateProductData(shopId, name, description, price, false, priceType))
            {
                var createdProduct = await productRepository.Create(shopId, name, description, priceType, price);
                return createdProduct;
            }
            else
            {
                throw new ArgumentException("Invalid products parameters for creation");
            }
        }

        public async Task<ProductRepository> UpdateProduct(string id, string shopId, string name, string description, string priceType, double? price)
        {
            if (validateData.Value.ValidateProductData(shopId, name, description, price, true, priceType, id))
            {
                var updatedProduct = await productRepository.Update(id, shopId, name, description, priceType, price);
                return updatedProduct;
            }
            else
            {
                throw new ArgumentException("Invalid products parameters for creation");
            }
        }

        public async Task<string> DeleteProduct(string id)
        {
            var products = await productRepository.FindOne(id);

            if (products != null)
            {
                var deletedProduct = await productRepository.Delete(id);
                return deletedProduct;
            }
            else
            {
                throw new ArgumentException("Invalid products parameters for creation");
            }
        }
    }
}
