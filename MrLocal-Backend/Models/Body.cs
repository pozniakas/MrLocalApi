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

        public class ShopBody
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Status { get; set; }
            public string Description { get; set; }
            public string TypeOfShop { get; set; }
            public string City { get; set; }
        }
        public class ProductBody
        {
            public string Id { get; set; }
            public string ShopId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public double? Price { get; set; }
            public string PriceType { get; set; }
        }
    }
}
