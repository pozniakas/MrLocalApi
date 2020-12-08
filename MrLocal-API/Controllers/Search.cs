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
        public RequestEventArgs ArgsForRequestEvents;
        public event EventHandler<RequestEventArgs> RequestStarted;
        public event EventHandler<RequestEventArgs> RequestFinished;
        private readonly SearchService searchService;
        private readonly ILoggerManager _logger;

        public Search(ILoggerManager logger)
        {
            _logger = logger;
            searchService = new SearchService();
            ArgsForRequestEvents = new RequestEventArgs(_logger, "api/search");
            RequestStarted += Event.RequestTriggeredHandler;
            RequestFinished += Event.RequestFinishedHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromBody] SearchBody body)
        {
            ArgsForRequestEvents._endpoint += "GET";
            RequestStarted.Invoke(sender: this, ArgsForRequestEvents);

            var search = await searchService.SearchForShops(body.SearchQuery, body.City, body.TypeOfShop);
            RequestFinished.Invoke(sender: this, ArgsForRequestEvents);
            return Ok(search);
        }
    }
}
