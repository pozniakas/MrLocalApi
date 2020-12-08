using Microsoft.AspNetCore.Mvc;
using MrLocal_API.Controllers.Interfaces;
using MrLocal_API.Controllers.LoggerService.Interfaces;
using MrLocal_API.Services;
using MrLocal_API.Services.Interfaces;
using System.Threading.Tasks;
using static MrLocal_API.Models.Body;

namespace MrLocal_API.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class Search : ControllerBase, ISearch
    {
        private readonly ISearchService _searchService;
        private readonly ILoggerManager _logger;

        public Search(ILoggerManager logger, ISearchService searchService)
        {
            _logger = logger;
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromBody] SearchBody body)
        {
            _logger.LogInfo("Searching for shop");

            var search = await _searchService.SearchForShops(body.SearchQuery, body.City, body.TypeOfShop);

            _logger.LogInfo("Returning shops");

            return Ok(search);
        }
    }
}
