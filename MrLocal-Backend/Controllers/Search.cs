using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Repositories;
using MrLocal_Backend.Services;
using System.Collections.Generic;

namespace MrLocal_Backend.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class Search : ApiController
    {
        private readonly SearchService searchService;

        public Search()
        {
            searchService = new SearchService();
        }

        [HttpGet]
        public List<ShopRepository> Get([FromBody] SearchBody body)
        {
            return searchService.SearchForShops(body.SearchQuery, body.City, body.TypeOfShop);
        }
    }
}
