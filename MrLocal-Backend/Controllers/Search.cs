using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Repositories;
using MrLocal_Backend.Services;
using System.Collections.Generic;
using static MrLocal_Backend.Models.Body;

namespace MrLocal_Backend.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class Search : ControllerBase
    {
        private readonly SearchService searchService;

        public Search()
        {
            searchService = new SearchService();
        }

        [HttpGet]
        public List<ShopRepository> Get([FromBody] SearchBody body) => searchService.SearchForShops(body.SearchQuery, body.City, body.TypeOfShop);   
    }
}
