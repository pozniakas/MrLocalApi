using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Repositories;
using MrLocal_Backend.Services;
using System.Collections.Generic;

namespace MrLocal_Backend.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class Search : ControllerBase
    {
        [HttpGet]
        public List<ShopRepository> Get([FromBody] GetArguments body)
        {
            var searchService = new SearchService();
            return searchService.SearchForShops(body.SearchQuery, body.City, body.TypeOfShop);
        }
    }

    public class GetArguments
    {
        public string SearchQuery { get; set; }
        public string City { get; set; }
        public string TypeOfShop { get; set; }

    }
}
