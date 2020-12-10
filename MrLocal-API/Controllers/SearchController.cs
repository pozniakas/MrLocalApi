using Microsoft.AspNetCore.Mvc;
using MrLocal_API.Controllers.Interfaces;
using MrLocal_API.Controllers.LoggerService.Interfaces;
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
        public IRequestEvent _requestEvents;

        public Search(ISearchService searchService, IRequestEvent requestEvent)
        {
            _searchService = searchService;
            _requestEvents = requestEvent;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromBody] SearchBody body)
        {
            _requestEvents.ReportAboutRequestStart("api/search GET");
            var search = await _searchService.SearchForShops(body.SearchQuery, body.City, body.TypeOfShop);
            _requestEvents.ReportAboutRequestFinish("api/search GET");
            return Ok(search);
        }
    }
}
