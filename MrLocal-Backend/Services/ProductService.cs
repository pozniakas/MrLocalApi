using MrLocal_Backend.Repositories;
using MrLocal_Backend.Services.Helpers;
using System;
using System.Threading.Tasks;

namespace MrLocal_Backend.Services
{
    class ProductService : ValidateData
    {
        private readonly ProductRepository productRepository;

        public ProductService()
        {
            productRepository = new ProductRepository();
        }

        public async Task<ProductRepository> AddProductToShop(string shopId, string name, string description, string priceType, double? price)
        {
            if (ValidateProductData(shopId, name, description, price, false, priceType))
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
            if (ValidateProductData(shopId, name, description, price, true, priceType, id))
            {
                var updatedProduct = await productRepository.Update(id, shopId, name, description, priceType, price);
                return updatedProduct;
            }
            else
            {
                throw new ArgumentException("Invalid products parameters for creation");
            }
        }

        public void DeleteProduct(string id)
        {
            var products = productRepository.FindOne(id);

            if (products != null)
            {
                productRepository.Delete(id);
            }
            else
            {
                throw new ArgumentException("Invalid products parameters for creation");
            }
        }
    }
}
