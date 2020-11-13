using MrLocal_Backend.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MrLocal_Backend.Services
{
    public class SearchService
    {
        private readonly ShopRepository shopRepository;

        public SearchService()
        {
            shopRepository = new ShopRepository();
        }

        public List<ShopRepository> SearchForShops(string searchQuery, string city = "All cities", string typeOfShop = "All types")
        {
            var shopList = shopRepository.FindAll().Where(i => CheckFilters(i, city, typeOfShop));
            var trimmedSearchQuery = searchQuery.Trim();
            var regex = new Regex(@"^(?=.*\b" + trimmedSearchQuery + @"\b).*$");

            return searchQuery.Length > 0 ? shopList.Where(i => regex.IsMatch(i.Name)
                || regex.IsMatch(i.TypeOfShop)
                || regex.IsMatch(i.City)).ToList() : shopList.ToList();
        }

        private bool CheckFilters(ShopRepository shop, string city, string typeOfShop)
        {
            return (city != "All cities" && typeOfShop != "All types" && shop.City == city && shop.TypeOfShop == typeOfShop)
                || (city != "All cities" && typeOfShop == "All types" && shop.City == city)
                || (city == "All cities" && typeOfShop != "All types" && shop.TypeOfShop == typeOfShop)
                || (city == "All cities" && typeOfShop == "All types");
        }

    }

}
