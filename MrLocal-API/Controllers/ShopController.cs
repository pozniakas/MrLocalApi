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
        public IRequestEvent _requestEvents;

        public ShopController(IShopService shopService, IRequestEvent requestEvent)
        {
            _shopService = shopService;
            _requestEvents = requestEvent;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            _requestEvents.ReportAboutRequestStart("api/shop GET");
            var getShop = await _shopService.GetShop(id);
            _requestEvents.ReportAboutRequestFinish("api/shop GET");
            return ReturnResponse(getShop);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Models.Shop body)
        {
            _requestEvents.ReportAboutRequestStart("api/shop POST");
            var createdShop = await _shopService.CreateShop(body.Name, body.Description, body.TypeOfShop, body.City);
            _requestEvents.ReportAboutRequestFinish("api/shop POST");
            return ReturnResponse(createdShop);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Models.Shop body)
        {
            _requestEvents.ReportAboutRequestStart("api/shop PUT");
            var updatedShop = await _shopService.UpdateShop(body.Id, body.Name, body.Status, body.Description, body.TypeOfShop, body.City);
            _requestEvents.ReportAboutRequestFinish("api/shop PUT");
            return ReturnResponse(updatedShop);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _requestEvents.ReportAboutRequestStart("api/shop DELETE");
            await _shopService.DeleteShop(id);
            _requestEvents.ReportAboutRequestFinish("api/shop DELETE");
            return ReturnResponse("Shop was deleted successfully");
        }

        public IActionResult ReturnResponse(object value) => Ok(new { Response = value });
    }
}
