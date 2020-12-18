using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MrLocalBackend.Models;
using MrLocalBackend.Repositories.Interfaces;
using MrLocalDb;
using MrLocalDb.Entities;

namespace MrLocalBackend.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MrLocalDbContext _context;
        private readonly Lazy<IEnumConverter> _enumConverter = null;

        public ProductRepository(Lazy<IEnumConverter> enumConverter, MrLocalDbContext context)
        {
            _context = context;
            _enumConverter = enumConverter;
        }

        public async Task<Product> Create(string shopId, string name
            , string description, string pricetype, decimal price)
        {
            var updatedAt = DateTime.UtcNow;
            var createdAt = DateTime.UtcNow;
            var id = Guid.NewGuid().ToString();
            var product = new Product(id, shopId, name, description, _enumConverter.Value.StringToPricetype(pricetype), price, createdAt, updatedAt);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> Update(string id, string shopId, string name
            , string description, string pricetype, decimal? price)
        {
            var dateNow = DateTime.UtcNow;
            var result = _context.Products.SingleOrDefault(b => b.ProductId == id && b.ShopId == shopId);

            static bool IsStringEmpty(string str) => str == null || str.Length == 0;

            result.Price = price != null ? (decimal)price : result.Price;
            result.Name = IsStringEmpty(name) ? result.Name : name;
            result.Description = IsStringEmpty(description) ? result.Description : description;
            result.PriceType = IsStringEmpty(pricetype) ? result.PriceType : _enumConverter.Value.StringToPricetype(pricetype);
            result.UpdatedAt = dateNow;

            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<string> Delete(string id)
        {
            var result = _context.Products.SingleOrDefault(b => b.ProductId == id);
            var dateNow = DateTime.UtcNow;

            result.DeletedAt = dateNow;

            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<Product> FindOne(string id)
        {
            var result = await _context.Products.SingleOrDefaultAsync(b => b.ProductId == id);

            return result;
        }

        public async Task<List<Product>> FindAll(string shopId)
        {
            var dbProducts = await _context.Products.Where(a => a.ShopId == shopId).ToListAsync();

            return dbProducts;
        }
    }
}
