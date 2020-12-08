using Microsoft.AspNetCore.Mvc;
using MrLocal_API.Controllers.Interfaces;
using MrLocal_API.Controllers.LoggerService.Interfaces;
using MrLocal_API.Services;
using System;
using System.Threading.Tasks;

namespace MrLocal_API.Controllers
{
    [Route("api/shop")]
    [ApiController]
    public class Shop : ControllerBase, IShop
    {
        public RequestEventArgs ArgsForRequestEvents;
        public event EventHandler<RequestEventArgs> RequestStarted;
        public event EventHandler<RequestEventArgs> RequestFinished;
        private readonly ShopService shopService;
        private readonly ILoggerManager _logger;
        
        public Shop(ILoggerManager logger)
        {
            _logger = logger;
            shopService = new ShopService();
            ArgsForRequestEvents = new RequestEventArgs(_logger, "api/shop");
            RequestStarted += Event.RequestTriggeredHandler;
            RequestFinished += Event.RequestFinishedHandler;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            ArgsForRequestEvents._endpoint += "GET";
            RequestStarted.Invoke(sender: this, ArgsForRequestEvents);

            var getShop = await shopService.GetShop(id);
            RequestFinished.Invoke(sender: this, ArgsForRequestEvents);
            return Ok(getShop);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Models.Shop body)
        {
            ArgsForRequestEvents._endpoint += "POST";
            RequestStarted.Invoke(sender: this, ArgsForRequestEvents);

            var createdShop = await shopService.CreateShop(body.Name, body.Description, body.TypeOfShop, body.City);
            RequestFinished.Invoke(sender: this, ArgsForRequestEvents);
            return Ok(createdShop);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Models.Shop body)
        {
            ArgsForRequestEvents._endpoint += "PUT";
            RequestStarted.Invoke(sender: this, ArgsForRequestEvents);

            var updatedShop = await shopService.UpdateShop(body.Id, body.Name, body.Status, body.Description, body.TypeOfShop, body.City);
            RequestFinished.Invoke(sender: this, ArgsForRequestEvents);
            return Ok(updatedShop);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            ArgsForRequestEvents._endpoint += "DELETE";
            RequestStarted.Invoke(sender: this, ArgsForRequestEvents);

            await shopService.DeleteShop(id);
            RequestFinished.Invoke(sender: this, ArgsForRequestEvents);
            return Ok("Shop was deleted successfully");

        }
    }
}
