using System.Text.Json.Serialization;
using MrLocalDb.Entities;

namespace MrLocalBackend.Models
{
    public class Body
    {
        public class SearchBody
        {
            public string SearchQuery { get; set; }
            public string City { get; set; }
            public string TypeOfShop { get; set; }
            public string Status { get; set; }
        }
        public class ProductBody
        {
            public string ProductId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            [JsonConverter(typeof(JsonStringEnumConverter))]
            public PriceTypes PriceType { get; set; }
            public string ShopId { get; set; }
        }

        public class ShopBody
        {
            public string ShopId { get; set; }
            public string Name { get; set; }
            public string Status { get; set; }
            public string Description { get; set; }
            public string TypeOfShop { get; set; }
            public string City { get; set; }
        }
    }
}
