﻿using MrLocal_API.Models;
using MrLocal_API.Repositories;
using MrLocal_API.Repositories.Interfaces;
using MrLocal_API.Services.Helpers;
using MrLocal_API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrLocal_API.Services
{
    class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IShopRepository _shopRepository;
        private readonly Lazy<ValidateData> validateData = null;

        public ProductService(IProductRepository productRepository, IShopRepository shopRepository)
        {
            _productRepository = productRepository;
            _shopRepository = shopRepository;
            validateData = new Lazy<ValidateData>();
        }

        public async Task<Product> AddProductToShop(string shopId, string name, string description, string priceType, double? price)
        {
            await validateData.Value.ValidateProductData(shopId, name, description, price, false, priceType);
            var createdProduct = await _productRepository.Create(shopId, name, description, priceType, price);
            return createdProduct;
        }

        public async Task<Product> UpdateProduct(string id, string shopId, string name, string description, string priceType, double? price)
        {
            var product = await _productRepository.FindOne(id);

            if (product == null)
            {
                throw new ArgumentException("Product to update doesn't exist");
            }

            await validateData.Value.ValidateProductData(shopId, name, description, price, true, priceType);
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