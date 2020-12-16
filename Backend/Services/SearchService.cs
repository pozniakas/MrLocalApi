using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class SearchService : ISearchService
    {

        private readonly IShopRepository _shopRepository;
        private readonly Lazy<IValidateData> _validateData = null;

        public SearchService(IShopRepository shopRepository, Lazy<IValidateData> validateData)
        {
            _shopRepository = shopRepository;
            _validateData = validateData;
        }

        public async Task<List<Shop>> SearchForShops(string searchQuery, string city = "All cities", string typeOfShop = "All types")
        {
            var shopList = (await _shopRepository.FindAll()).Where(i => _validateData.Value.ValidateFilters(i, city, typeOfShop));
            var trimmedSearchQuery = searchQuery.Trim();
            var regex = new Regex(@"^(?=.*\b" + trimmedSearchQuery + @"\b).*$");

            return searchQuery.Length > 0 ? shopList.Where(i => regex.IsMatch(i.Name)
                || regex.IsMatch(i.TypeOfShop)
                || regex.IsMatch(i.City)).ToList() : shopList.ToList();
        }
    }
}
