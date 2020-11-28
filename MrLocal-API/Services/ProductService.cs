﻿using MrLocal_API.Models;
using MrLocal_API.Repositories;
using MrLocal_API.Services.Helpers;
using MrLocal_API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrLocal_API.Services
{
    class ProductService : IProductService
    {
        private readonly ProductRepository productRepository;
        private readonly ShopRepository shopRepository;
        private readonly Lazy<ValidateData> validateData = null;

        public ProductService()
        {
            productRepository = new ProductRepository();
            shopRepository = new ShopRepository();
            validateData = new Lazy<ValidateData>();
        }

        public async Task<Product> AddProductToShop(string shopId, string name, string description, string priceType, double? price)
        {
            await validateData.Value.ValidateProductData(shopId, name, description, price, false, priceType);
            var createdProduct = await productRepository.Create(shopId, name, description, priceType, price);
            return createdProduct;
        }

        public async Task<Product> UpdateProduct(string id, string shopId, string name, string description, string priceType, double? price)
        {
            var product = await productRepository.FindOne(id);

            if (product == null)
            {
                throw new ArgumentException("Product to update doesn't exist");
            }

            await validateData.Value.ValidateProductData(shopId, name, description, price, true, priceType);
            var updatedProduct = await productRepository.Update(id, shopId, name, description, priceType, price);
            return updatedProduct;
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
                throw new ArgumentException("Invalid products parameters for deleting");
            }
        }

        public async Task<List<Product>> GetAllProducts(string shopId)
        {
            var shop = await shopRepository.FindOne(shopId);

            if (shop == null)
            {
                throw new ArgumentException("Couldn't get products of shop which doesn't exist");
            }

            var products = await productRepository.FindAll(shopId);
            return products;
        }
    }
}
