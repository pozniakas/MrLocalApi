using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Services;

namespace MrLocal_Backend.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class Product : ControllerBase
    {
        [HttpGet]
        public string Get([FromBody] GetProductArguments body)
        {
            return "Just a test";
        }

        [HttpPost]
        public string Post([FromBody] GetProductArguments body)
        {
            var productService = new ProductService();
            productService.AddProductToShop(body.ShopId, body.Name, body.Description, body.PriceType, body.Price);
            return "Successfully created";
        }

        [HttpPut]
        public string Put([FromBody] GetProductArguments body)
        {
            var productService = new ProductService();
            productService.UpdateProduct(body.Id, body.ShopId, body.Name, body.Description, body.PriceType, body.Price);
            return "Successfully updated";
        }

        [HttpDelete]
        public string Delete([FromBody] GetProductArguments body)
        {
            var productService = new ProductService();
            productService.DeleteProduct(body.Id);
            return "Successfully deleted";
        }

    }
    public class GetProductArguments
    {
        public string Id { get; set; }
        public string ShopId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string PriceType { get; set; }
    }
}