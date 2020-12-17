using Microsoft.AspNetCore.Mvc;
using MrLocalBackend.Services.Interfaces;
using MrLocalApi.Controllers.Interfaces;
using MrLocalApi.Controllers.LoggerService.Interfaces;
using System.Threading.Tasks;
using static MrLocalBackend.Models.Body;

namespace MrLocalApi.Controllers
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
            return ReturnResponse(search);
        }

        public IActionResult ReturnResponse(object value) => Ok(new { Response = value });
    }
}
