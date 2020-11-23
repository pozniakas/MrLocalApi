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
                shopService.CreateShop(body.Name, body.Description, body.TypeOfShop, body.City);
                return await shopService.GetShopByName(body.Name);
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
                shopService.UpdateShop(body.Id, body.Name, body.Status, body.Description, body.TypeOfShop, body.City);
                return await shopService.GetShopByName(body.Name);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ShopRepository> Delete(string id)
        {
            try
            {
                shopService.DeleteShop(id);
                return await shopService.GetShop(id);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
}
