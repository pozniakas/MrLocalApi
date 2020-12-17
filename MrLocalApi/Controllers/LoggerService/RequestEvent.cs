using MrLocalApi.Controllers.LoggerService.Interfaces;

namespace MrLocalApi.Controllers
{
    public class RequestEvent : IRequestEvent
    {
        public delegate void RequestStarted(object sender, RequestEventArgs e);
        public delegate void RequestFinished(object sender, RequestEventArgs e);
        public event RequestStarted RequestStartedEvent;
        public event RequestFinished RequestFinishedEvent;
        public RequestEventArgs Args;
        
        public RequestEvent(ILoggerManager logger)
        {
            RequestStartedEvent += RequestTriggeredHandler;
            RequestFinishedEvent += RequestFinishedHandler;
            Args = new RequestEventArgs(logger);
        }
        public void RequestTriggeredHandler(object sender, RequestEventArgs e) => e.Logger.LogInfo($"Endpoint ({e.Method}) request was started");
        public void RequestFinishedHandler(object sender, RequestEventArgs e) => e.Logger.LogInfo($"Endpoint ({e.Method}) request was finished");

        public void ReportAboutRequestStart(string method)
        {
            Args.Method = method;
            RequestStartedEvent?.Invoke(this, Args);
        }
        public void ReportAboutRequestFinish(string method)
        {
            Args.Method = method;
            RequestFinishedEvent?.Invoke(this, Args);
        }
    }

    public class RequestEventArgs
    {
        public ILoggerManager Logger { set; get; }
        public string Method { set; get; }

        public RequestEventArgs(ILoggerManager logger)
        {
            Logger = logger;
        }
    }
}