using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrLocalBackend.Models;
using MrLocalBackend.Repositories.Interfaces;
using MrLocalDb;

namespace MrLocalBackend.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly MrLocalDbContext _context;

        public ShopRepository(MrLocalDbContext context)
        {
            _context = context;
        }

        public async Task<Shop> Create(string name, string description, string typeOfShop, string city)
        {
            var updatedAt = DateTime.UtcNow;
            var createdAt = DateTime.UtcNow;
            var id = Guid.NewGuid().ToString();

            _context.Shops.Add(new MrLocalDb.Entities.Shop { ShopId = id, Name = name, Status = "Not Active", Description = description, TypeOfShop = typeOfShop, City = city, UpdatedAt = updatedAt, CreatedAt = createdAt, DeletedAt = null }); ;
            await _context.SaveChangesAsync();

            return new Shop(id, name, "Not Active", description, typeOfShop, city, createdAt, updatedAt);
        }

        public async Task<Shop> Update(string id, string name, string status, string description, string typeOfShop, string city)
        {
            static bool IsStringEmpty(string str) => str == null || str.Length == 0;

            var dateNow = DateTime.UtcNow;
            var result = _context.Shops.SingleOrDefault(b => b.ShopId == id);

            result.Name = IsStringEmpty(name) ? result.Name : name;
            result.Status = IsStringEmpty(status) ? result.Status : status;
            result.Description = IsStringEmpty(description) ? result.Description : description;
            result.TypeOfShop = IsStringEmpty(typeOfShop) ? result.TypeOfShop : typeOfShop;
            result.City = IsStringEmpty(city) ? result.City : city;
            result.UpdatedAt = dateNow;

            await _context.SaveChangesAsync();

            return new Shop(id, result.Name, result.Status, result.Description, result.TypeOfShop, result.City, result.CreatedAt, result.UpdatedAt);
        }

        public async Task<string> Delete(string id)

        {
            var result = _context.Shops.SingleOrDefault(b => b.ShopId == id);
            var dateNow = DateTime.UtcNow;

            result.DeletedAt = dateNow;

            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<Shop> FindOne(string id)
        {
            var result = _context.Shops.SingleOrDefault(b => b.ShopId == id);

            if (result != null)
            {
                var shop = new Shop(result.ShopId, result.Name, result.Status, result.Description, result.TypeOfShop, result.City, result.CreatedAt, result.UpdatedAt);

                return shop;
            }

            return null;
        }

        public async Task<List<Shop>> FindAll()
        {
            var dbShops = _context.Shops.ToList();
            var shops = new List<Shop>();

            foreach (var shop in dbShops)
            {
                shops.Add(new Shop(shop.ShopId, shop.Name, shop.Status, shop.Description, shop.TypeOfShop, shop.City, shop.CreatedAt, shop.UpdatedAt));
            }

            return shops;
        }
    }
}
