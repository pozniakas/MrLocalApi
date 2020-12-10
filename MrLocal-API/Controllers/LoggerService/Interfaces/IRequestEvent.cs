namespace MrLocal_API.Controllers.LoggerService.Interfaces
{
    public interface IRequestEvent
    {
        public void RequestTriggeredHandler(object sender, RequestEventArgs e);
        public void RequestFinishedHandler(object sender, RequestEventArgs e);
        public void ReportAboutRequestStart(string method);
        public void ReportAboutRequestFinish(string method);
    }
}
