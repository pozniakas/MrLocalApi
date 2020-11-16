using MrLocal_Backend.Repositories;
using MrLocal_Backend.Services.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MrLocal_Backend.Services
{
    public class SearchService : ValidateData
    {
        private readonly ShopRepository shopRepository;

        public SearchService()
        {
            shopRepository = new ShopRepository();
        }

        public List<ShopRepository> SearchForShops(string searchQuery, string city = "All cities", string typeOfShop = "All types")
        {
            var shopList = shopRepository.FindAll().Where(i => ValidateFilters(i, city, typeOfShop));
            var trimmedSearchQuery = searchQuery.Trim();
            var regex = new Regex(@"^(?=.*\b" + trimmedSearchQuery + @"\b).*$");

            return searchQuery.Length > 0 ? shopList.Where(i => regex.IsMatch(i.Name)
                || regex.IsMatch(i.TypeOfShop)
                || regex.IsMatch(i.City)).ToList() : shopList.ToList();
        }
    }
}
