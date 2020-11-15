namespace MrLocal_Backend.Controllers
{
    public class Helpers
    {
        public class GetSearchArguments
        {
            public string SearchQuery { get; set; }
            public string City { get; set; }
            public string TypeOfShop { get; set; }

        }

        public class GetShopArguments
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Status { get; set; }
            public string Description { get; set; }
            public string TypeOfShop { get; set; }
            public string City { get; set; }
        }
    }
}
