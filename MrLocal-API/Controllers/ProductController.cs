using Microsoft.AspNetCore.Mvc;
using MrLocal_API.Controllers.Interfaces;
using MrLocal_API.Controllers.LoggerService.Interfaces;
using MrLocal_API.Services;
using System;
using System.Threading.Tasks;

namespace MrLocal_API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase, IProduct
    {
        public RequestEvent RequestEvents;
        private readonly ProductService productService;
        public ProductController(ILoggerManager logger)
        {
            productService = new ProductService();
            RequestEvents = new RequestEvent(logger, "api/product");
        }

        [HttpGet("{shopId}")]
        public async Task<IActionResult> Get(string shopId)
        {
            RequestEvents.ReportAboutRequestStart("GET");
            var getProducts = await productService.GetAllProducts(shopId);
            RequestEvents.ReportAboutRequestFinish("GET");
            return Ok(getProducts);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Models.Product body)
        {
            RequestEvents.ReportAboutRequestStart("POST");
            var createdProduct = await productService.AddProductToShop(body.ShopId, body.Name, body.Description, body.PriceType.ToString(), body.Price);
            RequestEvents.ReportAboutRequestFinish("POST");
            return Ok(createdProduct);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Models.Product body)
        {
            RequestEvents.ReportAboutRequestStart("PUT");
            var updatedProduct = await productService.UpdateProduct(body.Id, body.ShopId, body.Name, body.Description, body.PriceType.ToString(), body.Price);
            RequestEvents.ReportAboutRequestFinish("PUT");
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            RequestEvents.ReportAboutRequestStart("DELETE");
            await productService.DeleteProduct(id);
            RequestEvents.ReportAboutRequestFinish("DELETE");
            return Ok("Product was deleted successfully");
        }
    }
}