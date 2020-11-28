
using MrLocal_Backend.Models;
using MrLocal_Backend.Repositories;
using MrLocal_Backend.Services.Helpers;
using MrLocal_Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MrLocal_Backend.Services
{
    public class SearchService : ISearchService
    {

        private readonly ShopRepository shopRepository;
        private readonly Lazy<ValidateData> validateData = null;

        public SearchService()
        {
            shopRepository = new ShopRepository();
            validateData = new Lazy<ValidateData>();
        }

        public async Task<List<Shop>> SearchForShops(string searchQuery, string city = "All cities", string typeOfShop = "All types")
        {
            var shopList = (await shopRepository.FindAll()).Where(i => validateData.Value.ValidateFilters(i, city, typeOfShop));
            var trimmedSearchQuery = searchQuery.Trim();
            var regex = new Regex(@"^(?=.*\b" + trimmedSearchQuery + @"\b).*$");

            return searchQuery.Length > 0 ? shopList.Where(i => regex.IsMatch(i.Name)
                || regex.IsMatch(i.TypeOfShop)
                || regex.IsMatch(i.City)).ToList() : shopList.ToList();
        }
    }
}
