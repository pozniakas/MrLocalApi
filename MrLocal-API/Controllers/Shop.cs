using Microsoft.AspNetCore.Mvc;
using MrLocal_API.Controllers.Interfaces;
using MrLocal_API.Controllers.LoggerService.Interfaces;
using MrLocal_API.Services;
using System.Threading.Tasks;

namespace MrLocal_API.Controllers
{
    [Route("api/shop")]
    [ApiController]
    public class Shop : ControllerBase, IShop
    {
        private readonly ShopService shopService;
        private readonly ILoggerManager _logger;

        public Shop(ILoggerManager logger)
        {
            _logger = logger;
            shopService = new ShopService();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            _logger.LogInfo($"Getting shop with id: {id}");
            var getShop = await shopService.GetShop(id);
            _logger.LogInfo($"Returning shop with id: {id}");
            return Ok(getShop);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Models.Shop body)
        {
            _logger.LogInfo("Creating shop");
            var createdShop = await shopService.CreateShop(body.Name, body.Description, body.TypeOfShop, body.City);
            _logger.LogInfo("Shop created");

            return Ok(createdShop);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Models.Shop body)
        {
            _logger.LogInfo("Updating shop");

            var updatedShop = await shopService.UpdateShop(body.Id, body.Name, body.Status, body.Description, body.TypeOfShop, body.City);

            _logger.LogInfo("Shop updated");

            return Ok(updatedShop);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInfo("Deleting shop");

            await shopService.DeleteShop(id);

            _logger.LogInfo("Shop deleted");

            return Ok("Shop was deleted successfully");

        }
    }
}
