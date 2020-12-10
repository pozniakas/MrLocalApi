using Microsoft.AspNetCore.Mvc;
using MrLocal_API.Controllers.Interfaces;
using MrLocal_API.Controllers.LoggerService.Interfaces;
using MrLocal_API.Services;
using System.Threading.Tasks;

namespace MrLocal_API.Controllers
{
    [Route("api/shop")]
    [ApiController]
    public class ShopController : ControllerBase, IShop
    {
        public RequestEvent RequestEvents;
        private readonly ShopService shopService;

        public ShopController(ILoggerManager logger)
        {
            shopService = new ShopService();
            RequestEvents = new RequestEvent(logger, "api/shop");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            RequestEvents.ReportAboutRequestStart("GET");
            var getShop = await shopService.GetShop(id);
            RequestEvents.ReportAboutRequestFinish("GET");
            return Ok(getShop);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Models.Shop body)
        {
            RequestEvents.ReportAboutRequestStart("POST");
            var createdShop = await shopService.CreateShop(body.Name, body.Description, body.TypeOfShop, body.City);
            RequestEvents.ReportAboutRequestFinish("POST");
            return Ok(createdShop);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Models.Shop body)
        {
            RequestEvents.ReportAboutRequestStart("PUT");
            var updatedShop = await shopService.UpdateShop(body.Id, body.Name, body.Status, body.Description, body.TypeOfShop, body.City);
            RequestEvents.ReportAboutRequestFinish("PUT");
            return Ok(updatedShop);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            RequestEvents.ReportAboutRequestStart("DELETE");
            await shopService.DeleteShop(id);
            RequestEvents.ReportAboutRequestFinish("DELETE");
            return Ok("Shop was deleted successfully");
        }
    }
}
