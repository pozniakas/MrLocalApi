using Microsoft.AspNetCore.Mvc;
using MrLocal_API.Controllers.Interfaces;
using MrLocal_API.Controllers.LoggerService.Interfaces;
using MrLocal_API.Services.Interfaces;
using System.Threading.Tasks;

namespace MrLocal_API.Controllers
{
    [Route("api/shop")]
    [ApiController]
    public class ShopController : ControllerBase, IShop
    {
        private readonly IShopService _shopService;
        private readonly ILoggerManager _logger;
        public RequestEvent RequestEvents;

        public ShopController(ILoggerManager logger, IShopService shopService)
        {
            _logger = logger;
            _shopService = shopService;
            RequestEvents = new RequestEvent(logger, "api/shop");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            RequestEvents.ReportAboutRequestStart("GET");
            var getShop = await _shopService.GetShop(id);
            RequestEvents.ReportAboutRequestFinish("GET");
            return Ok(getShop);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Models.Shop body)
        {
            RequestEvents.ReportAboutRequestStart("POST");
            var createdShop = await _shopService.CreateShop(body.Name, body.Description, body.TypeOfShop, body.City);
            RequestEvents.ReportAboutRequestFinish("POST");
            return Ok(createdShop);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Models.Shop body)
        {
            RequestEvents.ReportAboutRequestStart("PUT");
            var updatedShop = await _shopService.UpdateShop(body.Id, body.Name, body.Status, body.Description, body.TypeOfShop, body.City);
            RequestEvents.ReportAboutRequestFinish("PUT");
            return Ok(updatedShop);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            RequestEvents.ReportAboutRequestStart("DELETE");
            await _shopService.DeleteShop(id);
            RequestEvents.ReportAboutRequestFinish("DELETE");
            return Ok("Shop was deleted successfully");
        }
    }
}
