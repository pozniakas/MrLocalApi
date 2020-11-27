using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Controllers.Interfaces;
using MrLocal_Backend.Models;
using MrLocal_Backend.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MrLocal_Backend.Models.Body;

namespace MrLocal_Backend.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class Search : ControllerBase, ISearch
    {
        private readonly SearchService searchService;

        public Search()
        {
            searchService = new SearchService();
        }

        [HttpGet]
        public async Task<List<ShopModel>> Get([FromBody] SearchBody body) => await searchService.SearchForShops(body.SearchQuery, body.City, body.TypeOfShop);
    }
}
