using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Repositories;
using MrLocal_Backend.Services;
using System;

namespace MrLocal_Backend.Controllers
{
    [Route("api/shop")]
    [ApiController]
    public class Shop : ApiController
    {
        private readonly ShopService shopService;

        public Shop()
        {
            shopService = new ShopService();
        }

        [HttpGet]
        public ShopRepository Get([FromBody] ShopBody body)
        { 
            return shopService.GetShop(body.Id);
        }

        [HttpPost]
        public string Post([FromBody] ShopBody body)
        {
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
        public string Put([FromBody] ShopBody body)
        {
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
        public string Delete([FromBody] ShopBody body)
        {
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
