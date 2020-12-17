﻿using MrLocalBackend.Models;
using System.Threading.Tasks;

namespace MrLocalBackend.Services.Interfaces
{
    public interface IValidateData
    {
        public Task<bool> ValidateProductData(string shopId, string name, string description, decimal? price, bool isUpdate, string priceType);
        public Task<bool> ValidateShopData(string name, string status, string description, string typeOfShop, string city, bool isUpdate);
        public bool ValidateFilters(Shop shop, string city, string typeOfShop);
    }
}