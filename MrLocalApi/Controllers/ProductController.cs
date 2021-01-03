using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MrLocalApi.Controllers.Interfaces;
using MrLocalApi.Controllers.LoggerService.Interfaces;
using MrLocalBackend.Services.Interfaces;
using System.Threading.Tasks;
using static MrLocalBackend.Models.Body;

namespace MrLocalApi.Controllers
{
    [Route("api/product")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase, IProduct
    {
        private readonly IProductService _productService;
        private readonly IRequestEvent _requestEvents;

        public ProductController(IProductService productService, IRequestEvent requestEvent)
        {
            _productService = productService;
            _requestEvents = requestEvent;
        }

        [AllowAnonymous]
        [HttpGet("{shopId}")]
        public async Task<IActionResult> Get(string shopId)
        {
            _requestEvents.ReportAboutRequestStart("api/product GET");
            var getProducts = await _productService.GetAllProducts(shopId);
            _requestEvents.ReportAboutRequestFinish("api/product GET");
            return ReturnResponse(getProducts);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductBody body)
        {
            _requestEvents.ReportAboutRequestStart("api/product POST");
            var createdProduct = await _productService.AddProductToShop(body.ShopId, body.Name, body.Description, body.PriceType.ToString(), body.Price, HttpContext.User.Identity.Name);
            _requestEvents.ReportAboutRequestFinish("api/product POST");
            return ReturnResponse(createdProduct);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductBody body)
        {
            _requestEvents.ReportAboutRequestStart("api/product PUT");
            var updatedProduct = await _productService.UpdateProduct(body.ProductId, body.ShopId, body.Name, body.Description, body.PriceType.ToString(), body.Price, HttpContext.User.Identity.Name);
            _requestEvents.ReportAboutRequestFinish("api/product PUT");
            return ReturnResponse(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _requestEvents.ReportAboutRequestStart("api/product DELETE");
            await _productService.DeleteProduct(id, HttpContext.User.Identity.Name);
            _requestEvents.ReportAboutRequestFinish("api/product DELETE");
            return ReturnResponse("Product was deleted successfully");
        }

        public IActionResult ReturnResponse(object value) => Ok(new { Response = value });
    }
}