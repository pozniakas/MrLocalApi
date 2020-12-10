using Microsoft.AspNetCore.Mvc;
using MrLocal_API.Controllers.Interfaces;
using MrLocal_API.Controllers.LoggerService.Interfaces;
using MrLocal_API.Services.Interfaces;
using System.Threading.Tasks;

namespace MrLocal_API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase, IProduct
    {
        private readonly IProductService _productService;
        private readonly ILoggerManager _logger;
        private readonly RequestEvent RequestEvents;

        public ProductController(ILoggerManager logger, IProductService productService)
        {
            _productService = productService;
            RequestEvents = new RequestEvent(logger, "api/product");
        }

        [HttpGet("{shopId}")]
        public async Task<IActionResult> Get(string shopId)
        {
            RequestEvents.ReportAboutRequestStart("GET");
            var getProducts = await _productService.GetAllProducts(shopId);
            RequestEvents.ReportAboutRequestFinish("GET");
            return Ok(getProducts);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Models.Product body)
        {
            RequestEvents.ReportAboutRequestStart("POST");
            var createdProduct = await _productService.AddProductToShop(body.ShopId, body.Name, body.Description, body.PriceType.ToString(), body.Price);
            RequestEvents.ReportAboutRequestFinish("POST");
            return Ok(createdProduct);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Models.Product body)
        {
            RequestEvents.ReportAboutRequestStart("PUT");
            var updatedProduct = await _productService.UpdateProduct(body.Id, body.ShopId, body.Name, body.Description, body.PriceType.ToString(), body.Price);
            RequestEvents.ReportAboutRequestFinish("PUT");
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            RequestEvents.ReportAboutRequestStart("DELETE");
            await _productService.DeleteProduct(id);
            RequestEvents.ReportAboutRequestFinish("DELETE");
            return Ok("Product was deleted successfully");
        }
    }
}