using Microsoft.AspNetCore.Mvc;

namespace MrLocalBackend.Models
{
    public class Body : ControllerBase
    {
        public class SearchBody
        {
            public string SearchQuery { get; set; }
            public string City { get; set; }
            public string TypeOfShop { get; set; }
        }
    }
}
