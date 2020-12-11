using Newtonsoft.Json;

namespace MrLocal_API.Models
{
    public class ErrorDetails
    {
        public ApiError Error;
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ApiError
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
