using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Controllers.Interfaces;
using MrLocal_Backend.LoggerService;
using MrLocal_Backend.Repositories;
using MrLocal_Backend.Services;
using System;
using System.Threading.Tasks;
using static MrLocal_Backend.Models.Body;

namespace MrLocal_Backend.Controllers
{
    [Route("api/shop")]
    [ApiController]
    public class Shop : ControllerBase, IShop
    {
        private readonly ShopService shopService;
        private readonly ILoggerManager _logger;

        public Shop(ILoggerManager logger)
        {
            shopService = new ShopService();
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            _logger.LogInfo("Fetching all the Shops from the storage");

            var getShop = await shopService.GetShop(id);
            
            _logger.LogInfo("Returning shop");

            return Ok(getShop);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ShopBody body)
        {
            _logger.LogInfo("Fetching all the Shops from the storage");

            var createdShop = await shopService.CreateShop(body.Name, body.Description, body.TypeOfShop, body.City);

            _logger.LogInfo("Returning shops");

            return Ok(createdShop);     
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ShopBody body)
        {
            _logger.LogInfo("Fetching all the Shops from the storage");

            var updatedShop = await shopService.UpdateShop(body.Id, body.Name, body.Status, body.Description, body.TypeOfShop, body.City);

            _logger.LogInfo("Returning shops");

            return Ok(updatedShop);     
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInfo("Fetching all the Shops from the storage");

            await shopService.DeleteShop(id);

            _logger.LogInfo($"Deleting shop");

            return Ok();   
        }
    }
}
