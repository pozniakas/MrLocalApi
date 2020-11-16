namespace MrLocal_Backend.Controllers
{
    public class Arguments
    {
        public class GetSearch
        {
            public string SearchQuery { get; set; }
            public string City { get; set; }
            public string TypeOfShop { get; set; }
        }

        public class GetShop
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
