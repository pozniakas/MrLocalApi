using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Repositories;
using MrLocal_Backend.Services;
using System.Collections.Generic;

namespace MrLocal_Backend.Controllers
{
    [Route("api/shop")]
    [ApiController]
    public class Shop : ControllerBase
    {
        [HttpGet]
        public ShopRepository Get([FromBody] Helpers.GetShopArguments body)
        {
            var shopService = new ShopService();

            return shopService.GetShop(body.Id);
        }

        [HttpPost]
        public void Post([FromBody] Helpers.GetShopArguments body)
        {
            var shopService = new ShopService();

            shopService.CreateShop(body.Name, body.Description, body.TypeOfShop, body.City);
        }

        [HttpPut]
        public void Put([FromBody] Helpers.GetShopArguments body)
        {
            var shopService = new ShopService();

            shopService.UpdateShop(body.Id, body.Name, body.Status, body.Description, body.TypeOfShop, body.City);
        }

        [HttpDelete]
        public void Delete([FromBody] Helpers.GetShopArguments body)
        {
            var shopService = new ShopService();

            shopService.DeleteShop(body.Id);
        }
    }
}
