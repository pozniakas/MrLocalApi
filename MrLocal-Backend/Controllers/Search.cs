using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Repositories;
using MrLocal_Backend.Services;
using System.Collections.Generic;

namespace MrLocal_Backend.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class Search : Arguments
    {
        [HttpGet]
        public List<ShopRepository> Get([FromBody] GetSearch body)
        {
            var searchService = new SearchService();
            return searchService.SearchForShops(body.SearchQuery, body.City, body.TypeOfShop);
        }
    }
}
