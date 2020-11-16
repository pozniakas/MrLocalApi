using MrLocal_Backend.Repositories;
using MrLocal_Backend.Services.Helpers;
using System;

namespace MrLocal_Backend.Services
{
    class ProductService
    {
        private readonly ProductRepository productRepository;
        private readonly ValidateData validateData;

        public ProductService()
        {
            productRepository = new ProductRepository();
            validateData = new ValidateData();
        }

        public void AddProductToShop(string shopId, string name, string description, string priceType, double? price)
        {
            if (validateData.ValidateProductData(shopId, name, description, price, false, priceType))
            {
                productRepository.Create(shopId, name, description, priceType, price);
            }
            else
            {
                throw new ArgumentException("Invalid products parameters for creation");
            }
        }

        public void UpdateProduct(string id, string shopId, string name, string description, string priceType, double? price)
        {
            if (validateData.ValidateProductData(shopId, name, description, price, true, priceType, id))
            {
                productRepository.Update(id, shopId, name, description, priceType, price);
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
