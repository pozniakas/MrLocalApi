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
            await Task.Delay(0);
            try
            {
                productService.AddProductToShop(body.ShopId, body.Name, body.Description, body.PriceType, body.Price);
                return null;
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        [HttpPut]
        public async Task<ProductRepository> Put([FromBody] ProductBody body)
        {
            await Task.Delay(0);
            try
            {
                productService.UpdateProduct(body.Id, body.ShopId, body.Name, body.Description, body.PriceType, body.Price);
                return null;
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<string> Delete(string id)
        {
            await Task.Delay(0);

            try
            {
                productService.DeleteProduct(id);
                return "Product was deleted succesfully";
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message); ;
            }
        }
    }
}