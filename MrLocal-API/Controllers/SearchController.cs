using Microsoft.AspNetCore.Mvc;
using MrLocal_API.Controllers.Interfaces;
using MrLocal_API.Controllers.LoggerService.Interfaces;
using MrLocal_API.Services;
using System;
using System.Threading.Tasks;
using static MrLocal_API.Models.Body;

namespace MrLocal_API.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class Search : ControllerBase, ISearch
    {
        public RequestEvent RequestEvents;
        private readonly SearchService searchService;

        public Search(ILoggerManager logger)
        {
            searchService = new SearchService();
            RequestEvents = new RequestEvent(logger, "api/search");
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromBody] SearchBody body)
        {
            RequestEvents.ReportAboutRequestStart("GET");
            var search = await searchService.SearchForShops(body.SearchQuery, body.City, body.TypeOfShop);
            RequestEvents.ReportAboutRequestFinish("GET");
            return Ok(search);
        }
    }
}
