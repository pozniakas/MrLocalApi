using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Repositories;
using MrLocal_Backend.Services;
using System;
using System.Threading.Tasks;
using static MrLocal_Backend.Models.Body;

namespace MrLocal_Backend.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class Product : ControllerBase
    {
        private readonly ProductService productService;

        public Product()
        {
            productService = new ProductService();
        }

        [HttpPost]
        public async Task<ProductRepository> Post([FromBody] ProductBody body)
        {
            try
            {
                productService.AddProductToShop(body.ShopId, body.Name, body.Description, body.PriceType, body.Price);
                return await productService.GetProductByName(body.Name);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        [HttpPut]
        public async Task<ProductRepository> Put([FromBody] ProductBody body)
        {
            try
            {
                productService.UpdateProduct(body.Id, body.ShopId, body.Name, body.Description, body.PriceType, body.Price);
                return await productService.GetProductByName(body.Name);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ProductRepository> Delete(string id)
        {
            try
            {
                productService.DeleteProduct(id);
                return await productService.GetProduct(id);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message); ;
            }
        }
    }
}