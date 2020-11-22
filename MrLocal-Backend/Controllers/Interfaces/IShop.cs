using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Repositories;
using static MrLocal_Backend.Models.Body;

namespace MrLocal_Backend.Controllers.Interfaces
{
    interface IShop
    {
        public ShopRepository Get(string id);
        public string Post([FromBody] ShopBody body);
        public string Put([FromBody] ShopBody body);
        public string Delete(string id);
    }
}
