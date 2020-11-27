using Microsoft.AspNetCore.Mvc;

namespace MrLocal_Backend.Models
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
