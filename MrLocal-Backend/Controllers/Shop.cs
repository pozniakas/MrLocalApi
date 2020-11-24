using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Repositories;
using MrLocal_Backend.Services;
using System;
using System.Threading.Tasks;
using static MrLocal_Backend.Models.Body;

namespace MrLocal_Backend.Controllers
{
    [Route("api/shop")]
    [ApiController]
    public class Shop : ControllerBase
    {
        private readonly ShopService shopService;

        public Shop()
        {
            shopService = new ShopService();
        }

        [HttpGet("{id}")]
        public async Task<ShopRepository> GetAsync(string id)
        {
            return await shopService.GetShop(id);
        }

        [HttpPost]
        public async Task<ShopRepository> Post([FromBody] ShopBody body)
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
        public async Task<ShopRepository> Put([FromBody] ShopBody body)
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
            await Task.Delay(0);

            try
            {
                shopService.DeleteShop(id);
                return "Shop was deleted succesfully";
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
}
