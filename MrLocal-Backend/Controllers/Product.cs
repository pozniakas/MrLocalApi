using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Controllers.Interfaces;
using MrLocal_Backend.Services;
using System;
using System.Threading.Tasks;

namespace MrLocal_Backend.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class Product : ControllerBase, IProduct
    {
        private readonly ProductService productService;

        public Product()
        {
            productService = new ProductService();
        }

        [HttpPost]
        public async Task<Models.Product> Post([FromBody] Models.Product body)
        {
            try
            {
                var createdProduct = await productService.AddProductToShop(body.ShopId, body.Name, body.Description, body.PriceType.ToString(), body.Price);
                return createdProduct;
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        [HttpPut]
        public async Task<Models.Product> Put([FromBody] Models.Product body)
        {
            try
            {
                var updatedProduct = await productService.UpdateProduct(body.Id, body.ShopId, body.Name, body.Description, body.PriceType.ToString(), body.Price); ;
                return updatedProduct;
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<string> Delete(string id)
        {
            try
            {
                await productService.DeleteProduct(id);
                return "Product was deleted succesfully";
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
}