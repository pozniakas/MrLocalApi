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
            _logger = logger;
            productService = new ProductService();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductBody body)
        {
            _logger.LogInfo("Creating product");

            var createdProduct = await productService.AddProductToShop(body.ShopId, body.Name, body.Description, body.PriceType, body.Price);

            _logger.LogInfo("Product created");

            return Ok(createdProduct);

        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductBody body)
        {
            _logger.LogInfo("Updating product");

            var updatedProduct = await productService.UpdateProduct(body.Id, body.ShopId, body.Name, body.Description, body.PriceType, body.Price);

            _logger.LogInfo("Product updated");

            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInfo("Deleting shop");

            await productService.DeleteProduct(id);

            _logger.LogInfo("Product deleted");

            return Ok();
        }
    }
}