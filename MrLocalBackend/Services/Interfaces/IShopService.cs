﻿using System.Threading.Tasks;
using MrLocalDb.Entities;

namespace MrLocalBackend.Services.Interfaces
{
    public interface IShopService
    {
        public Task<Shop> CreateShop(string name, string description, string typeOfShop, string city);
        public Task<Shop> UpdateShop(string id, string name, string status, string description, string typeOfShop, string city);
        public Task<string> DeleteShop(string id);
        public Task<Shop> GetShop(string id);
    }
}
