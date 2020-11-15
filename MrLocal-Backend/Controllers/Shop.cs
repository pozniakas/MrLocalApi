using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Repositories;
using MrLocal_Backend.Services;
using System;

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
        public string Post([FromBody] Helpers.GetShopArguments body)
        {
            var shopService = new ShopService();

            try
            {
                shopService.CreateShop(body.Name, body.Description, body.TypeOfShop, body.City);

                return "Shop was created successfully";
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
        }

        [HttpPut]
        public string Put([FromBody] Helpers.GetShopArguments body)
        {
            var shopService = new ShopService();

            try
            {
                shopService.UpdateShop(body.Id, body.Name, body.Status, body.Description, body.TypeOfShop, body.City);

                return "Shop was updated successfully";

            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
        }

        [HttpDelete]
        public string Delete([FromBody] Helpers.GetShopArguments body)
        {
            var shopService = new ShopService();
            try
            {
                shopService.DeleteShop(body.Id);

                return "Shop was deleted succesfully";
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }

        }
    }
}
