using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Controllers.Interfaces;
using MrLocal_Backend.Services;
using System;
using static MrLocal_Backend.Models.Body;

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
        public string Post([FromBody] ProductBody body)
        {
            try
            {
                productService.AddProductToShop(body.ShopId, body.Name, body.Description, body.PriceType, body.Price);
                return "Product was created successfully";
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
        }

        [HttpPut]
        public string Put([FromBody] ProductBody body)
        {
            try
            {
                productService.UpdateProduct(body.Id, body.ShopId, body.Name, body.Description, body.PriceType, body.Price);
                return "Product was updated successfully";
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
        }

        [HttpDelete("{id}")]
        public string Delete(string id)
        {
            try
            {
                productService.DeleteProduct(id);
                return "Product was deleted succesfully";
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
        }
    }
}