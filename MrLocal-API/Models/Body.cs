using Microsoft.AspNetCore.Mvc;

namespace MrLocal_API.Models
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
