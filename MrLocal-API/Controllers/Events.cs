using MrLocal_API.Controllers.LoggerService.Interfaces;
using System;

namespace MrLocal_API.Controllers
{
    public static class Event
    {
        public static void RequestTriggeredHandler(object sender, RequestEventArgs e) => e._logger.LogInfo($"Endpoint ({e._endpoint}) request was started");
        public static void RequestFinishedHandler(object sender, RequestEventArgs e) => e._logger.LogInfo($"Endpoint ({e._endpoint}) request was finished");
    }
    public class RequestEventArgs : EventArgs
    {
        public ILoggerManager _logger { set; get; }
        public string _endpoint { set; get; }

        public RequestEventArgs(ILoggerManager logger, string endpoint)
        {
            _logger = logger;
            _endpoint = $"{endpoint} ";
        }
    }
}
