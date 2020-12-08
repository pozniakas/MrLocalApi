using Microsoft.AspNetCore.Mvc;
using MrLocal_API.Controllers.Interfaces;
using MrLocal_API.Controllers.LoggerService.Interfaces;
using MrLocal_API.Services;
using MrLocal_API.Services.Interfaces;
using System.Threading.Tasks;

namespace MrLocal_API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class Product : ControllerBase, IProduct
    {
        private readonly IProductService _productService;
        private readonly ILoggerManager _logger;

        public Product(ILoggerManager logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet("{shopId}")]
        public async Task<IActionResult> Get(string shopId)
        {
            _logger.LogInfo($"Getting products with this shop Id: {shopId}");

            var getProducts = await _productService.GetAllProducts(shopId);

            _logger.LogInfo($"Returning products with this shop Id: {shopId}");

            return Ok(getProducts);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Models.Product body)
        {
            _logger.LogInfo("Creating product");

            var createdProduct = await _productService.AddProductToShop(body.ShopId, body.Name, body.Description, body.PriceType.ToString(), body.Price);

            _logger.LogInfo("Product created");

            return Ok(createdProduct);

        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Models.Product body)
        {
            _logger.LogInfo("Updating product");

            var updatedProduct = await _productService.UpdateProduct(body.Id, body.ShopId, body.Name, body.Description, body.PriceType.ToString(), body.Price);

            _logger.LogInfo("Product updated");

            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInfo("Deleting shop");

            await _productService.DeleteProduct(id);

            _logger.LogInfo("Product deleted");

            return Ok("Product was deleted successfully");
        }
    }
}