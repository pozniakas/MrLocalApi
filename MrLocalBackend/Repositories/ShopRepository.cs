using Microsoft.EntityFrameworkCore;
using MrLocalBackend.Repositories.Interfaces;
using MrLocalDb;
using MrLocalDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrLocalBackend.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly MrLocalDbContext _context;
        private readonly IProductRepository _productRepository;

        public ShopRepository(MrLocalDbContext context, IProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }

        private static Shop CheckForProducts(Shop shop)
        {
            if (shop.Product == null)
            {
                shop.Product = new List<Product>();
            }

            return shop;
        }

        public async Task<Shop> Create(string name, string description, string typeOfShop, string city)
        {
            var updatedAt = DateTime.UtcNow;
            var createdAt = DateTime.UtcNow;
            var id = Guid.NewGuid().ToString();
            var shop = new Shop(id, name, "Not Active", description, typeOfShop, city, createdAt, updatedAt);

            _context.Shops.Add(shop);
            await _context.SaveChangesAsync();

            return shop != null ? CheckForProducts(shop) : shop;
        }

        public async Task<Shop> Update(string id, string name, string status, string description, string typeOfShop, string city, Product[] listOfNewProducts)
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

            var previousShopProducts = await _productRepository.FindAll(id);
            var deletedShopProducts = previousShopProducts.Where(a => listOfNewProducts.Where(b => a.ProductId == b.ProductId).Count() == 0).ToList();
            var addedShopProducts = listOfNewProducts.Where(a => a.ProductId == null).ToList();

            foreach (var product in deletedShopProducts)
            {
                await _productRepository.Delete(product.ProductId);
            }

            foreach (var product in addedShopProducts)
            {
                await _productRepository.Create(product.ShopId, product.Name, product.Description, product.PriceType.ToString(), product.Price);
            }

            await _context.SaveChangesAsync();

            var newResult = _context.Shops.SingleOrDefault(b => b.ShopId == id);

            return newResult != null ? CheckForProducts(newResult) : newResult;
        }

        public async Task<string> Delete(string id)

        {
            var result = _context.Shops.SingleOrDefault(b => b.ShopId == id);
            var dateNow = DateTime.UtcNow;

            _context.Remove(result);

            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<Shop> FindOne(string id)
        {
            var result = await _context.Shops.Include(b => b.Product).SingleOrDefaultAsync(b => b.ShopId == id);

            return result != null ? CheckForProducts(result) : result;
        }

        public async Task<List<Shop>> FindAll()
        {
            var dbShops = await _context.Shops.Include(b => b.Product).ToListAsync();

            return dbShops.Select(shop => CheckForProducts(shop)).ToList();
        }
    }
}
