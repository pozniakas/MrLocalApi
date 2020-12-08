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
    public class Product : ControllerBase, IProduct
    {
        public RequestEventArgs ArgsForRequestEvents;
        public event EventHandler<RequestEventArgs> RequestStarted;
        public event EventHandler<RequestEventArgs> RequestFinished;
        private readonly ProductService productService;
        private readonly ILoggerManager _logger;
        public Product(ILoggerManager logger)
        {
            _logger = logger;
            productService = new ProductService();
            ArgsForRequestEvents = new RequestEventArgs(_logger, "api/product");
            RequestStarted += Event.RequestTriggeredHandler;
            RequestFinished += Event.RequestFinishedHandler;
        }

        [HttpGet("{shopId}")]
        public async Task<IActionResult> Get(string shopId)
        {
            ArgsForRequestEvents._endpoint += "GET";
            RequestStarted.Invoke(sender: this, ArgsForRequestEvents);

            var getProducts = await productService.GetAllProducts(shopId);
            RequestFinished.Invoke(sender: this, ArgsForRequestEvents);
            return Ok(getProducts);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Models.Product body)
        {
            ArgsForRequestEvents._endpoint += "POST";
            RequestStarted.Invoke(sender: this, ArgsForRequestEvents);

            var createdProduct = await productService.AddProductToShop(body.ShopId, body.Name, body.Description, body.PriceType.ToString(), body.Price);
            RequestFinished.Invoke(sender: this, ArgsForRequestEvents);
            return Ok(createdProduct);

        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Models.Product body)
        {
            ArgsForRequestEvents._endpoint += "PUT";
            RequestStarted.Invoke(sender: this, ArgsForRequestEvents);

            var updatedProduct = await productService.UpdateProduct(body.Id, body.ShopId, body.Name, body.Description, body.PriceType.ToString(), body.Price);
            RequestFinished.Invoke(sender: this, ArgsForRequestEvents);
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            ArgsForRequestEvents._endpoint += "DELETE";
            RequestStarted.Invoke(sender: this, ArgsForRequestEvents);

            await productService.DeleteProduct(id);
            RequestFinished.Invoke(sender: this, ArgsForRequestEvents);
            return Ok("Product was deleted successfully");
        }
    }
}