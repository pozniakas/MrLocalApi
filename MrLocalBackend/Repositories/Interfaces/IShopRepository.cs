﻿using MrLocalDb.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrLocalBackend.Repositories.Interfaces
{
    public interface IShopRepository
    {
        public Task<Shop> Create(string name, string description, string typeOfShop, string phone, string city, string userId);
        public Task<Shop> Update(string id, string name, string status, string description, string typeOfShop, string phone, string city, Product[] products);
        public Task<string> Delete(string id);
        public Task<Shop> FindOne(string id);
        public Task<List<Shop>> FindAll();
    }
}
