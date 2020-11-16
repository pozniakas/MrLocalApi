using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Services;
using System;

namespace MrLocal_Backend.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class Product : ApiController
    {
        [HttpPost]
        public string Post([FromBody] GetProduct body)
        {
            try
            {
                var productService = new ProductService();
                productService.AddProductToShop(body.ShopId, body.Name, body.Description, body.PriceType, body.Price);
                return "Product was created successfully";
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
        }

        [HttpPut]
        public string Put([FromBody] GetProduct body)
        {
            try
            {
                var productService = new ProductService();
                productService.UpdateProduct(body.Id, body.ShopId, body.Name, body.Description, body.PriceType, body.Price);
                return "Product was updated successfully";
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
        }

        [HttpDelete]
        public string Delete([FromBody] GetProduct body)
        {
            try
            {
                var productService = new ProductService();
                productService.DeleteProduct(body.Id);
                return "Product was deleted succesfully";
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
        }
    }
}