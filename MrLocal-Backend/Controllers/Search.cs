using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Controllers.Interfaces;
using MrLocal_Backend.LoggerService;
using MrLocal_Backend.Repositories;
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
        private readonly ILoggerManager _logger;

        public Search(ILoggerManager logger)
        {
            searchService = new SearchService();
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromBody] SearchBody body)
        {
            _logger.LogInfo("Fetching all shops from the storage");

            var search = await searchService.SearchForShops(body.SearchQuery, body.City, body.TypeOfShop);

            _logger.LogInfo("Returning shops");

            return Ok(search);
        }
    }
}
