using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Controllers.Interfaces;
using MrLocal_Backend.LoggerService;
using MrLocal_Backend.Services;
using System.Threading.Tasks;
using static MrLocal_Backend.Models.Body;

namespace MrLocal_Backend.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class Product : ControllerBase, IProduct
    {
        private readonly ProductService productService;
        private readonly ILoggerManager _logger;

        public Product(ILoggerManager logger)
        {
            productService = new ProductService();
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductBody body)
        {
            _logger.LogInfo("Fetching all the Products from the storage");

            var createdProduct = await productService.AddProductToShop(body.ShopId, body.Name, body.Description, body.PriceType, body.Price);

            _logger.LogInfo("Returning products");

            return Ok(createdProduct);

        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductBody body)
        {
            _logger.LogInfo("Fetching all the Products from the storage");

            var updatedProduct = await productService.UpdateProduct(body.Id, body.ShopId, body.Name, body.Description, body.PriceType, body.Price);

            _logger.LogInfo("Returning products");

            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInfo("Fetching all the Products from the storage");

            await productService.DeleteProduct(id);

            _logger.LogInfo("Deleting product");

            return Ok();
        }
    }
}