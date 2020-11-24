using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Controllers.Interfaces;
using MrLocal_Backend.Repositories;
using MrLocal_Backend.Services;
using System;
using static MrLocal_Backend.Models.Body;

namespace MrLocal_Backend.Controllers
{
    [Route("api/shop")]
    [ApiController]
    public class Shop : ControllerBase, IShop
    {
        private readonly ShopService shopService;

        public Shop()
        {
            shopService = new ShopService();
        }

        [HttpGet("{id}")]
        public ShopRepository Get(string id)
        {
            return shopService.GetShop(id);
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

        [HttpDelete("{id}")]
        public string Delete(string id)
        {
            try
            {
                shopService.DeleteShop(id);
                return "Shop was deleted succesfully";
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
        }
    }
}
