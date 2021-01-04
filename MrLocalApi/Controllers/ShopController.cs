using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MrLocalApi.Controllers.Interfaces;
using MrLocalApi.Controllers.LoggerService.Interfaces;
using MrLocalBackend.Services.Interfaces;
using System.Threading.Tasks;
using static MrLocalBackend.Models.Body;

namespace MrLocalApi.Controllers
{
    [Route("api/shop")]
    [ApiController]
    [Authorize]
    public class ShopController : ControllerBase, IShop
    {
        private readonly IShopService _shopService;
        public IRequestEvent _requestEvents;

        public ShopController(IShopService shopService, IRequestEvent requestEvent)
        {
            _shopService = shopService;
            _requestEvents = requestEvent;

        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            _requestEvents.ReportAboutRequestStart("api/shop GET");
            var getShop = await _shopService.GetShop(id);
            _requestEvents.ReportAboutRequestFinish("api/shop GET");
            return ReturnResponse(getShop);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ShopBody body)
        {
            _requestEvents.ReportAboutRequestStart("api/shop POST");
            var createdShop = await _shopService.CreateShop(body.Name, body.Description, body.TypeOfShop,body.Phone, body.Latitude, body.Longitude, body.City, HttpContext.User.Identity.Name);
            _requestEvents.ReportAboutRequestFinish("api/shop POST");
            return ReturnResponse(createdShop);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ShopBody body)
        {
            _requestEvents.ReportAboutRequestStart("api/shop PUT");
            var updatedShop = await _shopService.UpdateShop(body.ShopId, body.Name, body.Status, body.Description, body.TypeOfShop,body.Phone, body.City, body.Product, HttpContext.User.Identity.Name);
            _requestEvents.ReportAboutRequestFinish("api/shop PUT");
            return ReturnResponse(updatedShop);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _requestEvents.ReportAboutRequestStart("api/shop DELETE");
            await _shopService.DeleteShop(id, HttpContext.User.Identity.Name);
            _requestEvents.ReportAboutRequestFinish("api/shop DELETE");
            return ReturnResponse("Shop was deleted successfully");
        }

        public IActionResult ReturnResponse(object value) => Ok(new { Response = value });
    }
}
