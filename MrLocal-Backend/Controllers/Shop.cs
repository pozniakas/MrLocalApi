using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Controllers.Interfaces;
using MrLocal_Backend.Models;
using MrLocal_Backend.Services;
using System;
using System.Threading.Tasks;

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
        public async Task<ShopModel> Get(string id)
        {
            return await shopService.GetShop(id);
        }

        [HttpPost]
        public async Task<ShopModel> Post([FromBody] ShopModel body)
        {
            try
            {
                var createdShop = await shopService.CreateShop(body.Name, body.Description, body.TypeOfShop, body.City);
                return createdShop;
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        [HttpPut]
        public async Task<ShopModel> Put([FromBody] ShopModel body)
        {
            try
            {
                var updatedShop = await shopService.UpdateShop(body.Id, body.Name, body.Status, body.Description, body.TypeOfShop, body.City);
                return updatedShop;
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<string> Delete(string id)
        {
            try
            {
                await shopService.DeleteShop(id);
                return "Shop was deleted succesfully";
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
}
