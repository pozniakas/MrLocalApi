using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MrLocalBackend.Repositories.Interfaces;
using MrLocalDb;
using MrLocalDb.Entities;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<Shop> Create(string name, string description, string typeOfShop, string phone, string city, string userId)
        {
            var updatedAt = DateTime.UtcNow;
            var createdAt = DateTime.UtcNow;
            var shopId = Guid.NewGuid().ToString();
            var status = "Not Active";
            var shop = new Shop(shopId, name, status, description, typeOfShop, phone, city, createdAt, updatedAt, userId);

            var connection = "Server=localhost\\SQLEXPRESS;Database=MrLocal;Trusted_Connection=True";

            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = connection;
                try
                {
                    cn.Open();
                    Console.WriteLine("Connection opened");
                    using (var command = new SqlCommand())
                    {
                        command.Connection = cn;
                        command.CommandType = CommandType.Text;
                        command.CommandText = $"INSERT INTO Shops(ShopId,Name,Status,Description,TypeOfShop,Phone,City,CreatedAt,UpdatedAt,UserId) VALUES ('{shopId}','{name}','{status}','{description}', '{typeOfShop}','{phone}','{city}','{createdAt}','{updatedAt}','{userId}')";

                        var rowsAffectedCount = await command.ExecuteNonQueryAsync();
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                }
                catch (Exception e)
                {
                    // log it here
                    Console.WriteLine(e);
                }
                finally
                {
                    cn.Close();
                    Console.WriteLine("Connection closed");
                }

            }

            return shop != null ? CheckForProducts(shop) : shop;
        }

        public async Task<Shop> Update(string id, string name, string status, string description, string typeOfShop, string phone, string city, Product[] listOfNewProducts)
        {
            static bool IsStringEmpty(string str) => str == null || str.Length == 0;

            var dateNow = DateTime.UtcNow;
            var result = _context.Shops.SingleOrDefault(b => b.ShopId == id);

            result.Name = IsStringEmpty(name) ? result.Name : name;
            result.Status = IsStringEmpty(status) ? result.Status : status;
            result.Description = IsStringEmpty(description) ? result.Description : description;
            result.TypeOfShop = IsStringEmpty(typeOfShop) ? result.TypeOfShop : typeOfShop;
            result.Phone = IsStringEmpty(phone) ? result.Phone : phone;
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
            var result = await _context.Shops.Include(b => b.Product).Include(b => b.Location).SingleOrDefaultAsync(b => b.ShopId == id);

            return result != null ? CheckForProducts(result) : result;
        }

        public async Task<List<Shop>> FindAll()
        {
            var dbShops = await _context.Shops.Include(b => b.Product).Include(b => b.Location).ToListAsync();

            return dbShops.Select(shop => CheckForProducts(shop)).ToList();
        }
    }
}
