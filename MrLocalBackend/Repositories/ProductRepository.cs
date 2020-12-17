using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrLocalBackend.Models;
using MrLocalBackend.Repositories.Interfaces;
using MrLocalDb;

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

            _context.Products.Add(new MrLocalDb.Entities.Product { ProductId = id, Name = name, Description = description, UpdatedAt = updatedAt, CreatedAt = createdAt, DeletedAt = null, PriceType = pricetype, ShopId = shopId, Price = price }); ;
            await _context.SaveChangesAsync();

            return new Product(id, shopId, name, description, _enumConverter.Value.StringToPricetype(pricetype), price, createdAt, updatedAt);
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
            result.PriceType = IsStringEmpty(pricetype) ? result.PriceType : pricetype;
            result.UpdatedAt = dateNow;

            await _context.SaveChangesAsync();

            return new Product(id, shopId, result.Name, result.Description, _enumConverter.Value.StringToPricetype(result.PriceType), result.Price, result.CreatedAt, dateNow);
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
            var result = _context.Products.SingleOrDefault(b => b.ProductId == id);

            if (result != null)
            {
                var product = new Product { Id = result.ProductId, Name = result.Name, Description = result.Description, ShopId = result.ShopId, PriceType = _enumConverter.Value.StringToPricetype(result.PriceType), Price = result.Price, CreatedAt = result.CreatedAt, UpdatedAt = result.UpdatedAt, DeletedAt = result.DeletedAt };

                return product;
            }
            return null;
        }

        public async Task<List<Product>> FindAll(string shopId)
        {
            var dbProducts = _context.Products.Where(a => a.ShopId == shopId).ToList();
            var products = new List<Product>();
            foreach (var product in dbProducts)
            {
                products.Add(new Product(product.ProductId, product.ShopId, product.Name, product.Description, _enumConverter.Value.StringToPricetype(product.PriceType), product.Price, product.CreatedAt, product.UpdatedAt));
            }

            return products;
        }
    }
}
