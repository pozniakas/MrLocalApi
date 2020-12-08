using MrLocal_API.Controllers.LoggerService.Interfaces;
using System;

namespace MrLocal_API.Controllers
{
    public class Event
    {
        public Event()
        {

        }

        public delegate void RequestStarted(
  object sender, RequestArgs e);

        public event RequestStarted requestStarted;

        public void RequestStartedHandler(object Sender, RequestArgs e)
        {
            e._logger.LogInfo($"Endpoint {e._endpoint} request was started");

        }

    }
    public class RequestArgs : EventArgs
    {
        public ILoggerManager _logger { set; get; }
        public string _endpoint { set; get; }

        public RequestArgs(ILoggerManager logger, string endpoint)
        {
            _logger = logger;
            _endpoint = endpoint;
        }
    }
}
