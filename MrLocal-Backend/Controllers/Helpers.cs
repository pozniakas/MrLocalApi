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
            public string Id { get; private set; }
            public string Name { get; private set; }
            public string Status { get; private set; }
            public string Description { get; private set; }
            public string TypeOfShop { get; private set; }
            public string City { get; private set; }
        }
    }
}
