
﻿using MrLocal_Backend.Models;
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

        public ShopService()
        {
            validateData = new Lazy<ValidateData>();
            shopRepository = new ShopRepository();
        }

        public async Task<Shop> CreateShop(string name, string description, string typeOfShop, string city)
        {
            var isValidated = await validateData.Value.ValidateShopData(name, null, description, typeOfShop, city, false);

            if (isValidated)
            {
                var createdShop = await shopRepository.Create(name, description, typeOfShop, city);
                return createdShop;
            }
            else
            {
                throw new ArgumentException("Invalid shop parameters for creation");
            }
        }

        public async Task<Shop> UpdateShop(string id, string name, string status, string description, string typeOfShop, string city)
        {

            var isValidated = await validateData.Value.ValidateShopData(name, status, description, typeOfShop, city, true);

            if (isValidated)

            {
                var updatedShop = await shopRepository.Update(id, name, status, description, typeOfShop, city);
                return updatedShop;
            }
            else
            {
                throw new ArgumentException("Invalid shop parameters for update");
            }
        }

        public async Task<string> DeleteShop(string id)
        {
            var shop = await shopRepository.FindOne(id);

            if (shop != null)
            {
                var deletedShop = await shopRepository.Delete(id);
                return deletedShop;
            }
            else
            {
                throw new ArgumentException("Invalid id for deleting shop");
            }
        }

        public async Task<Shop> GetShop(string id)
        {
            var shop = await shopRepository.FindOne(id);

            if (shop == null)
            {
                throw new ArgumentException("Invalid id for getting shop");
            }

            return shop;
        }
    }
}
