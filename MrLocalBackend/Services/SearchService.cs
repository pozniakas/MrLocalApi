using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MrLocalBackend.Repositories.Interfaces;
using MrLocalBackend.Services.Interfaces;
using MrLocalDb.Entities;

namespace MrLocalBackend.Services
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

        public async Task<List<Shop>> SearchForShops(string searchQuery, string status, string city = "All cities", string typeOfShop = "All types")
        {
            string[] statusList = { "All shops", "Active", "Not Active" };
            var shopList = (await _shopRepository.FindAll()).Where(i => _validateData.Value.ValidateFilters(i, city, typeOfShop));
            var correctStatusShopList = status == statusList[0] ? shopList : status == statusList[1] ? shopList.Where(a => a.Status == "Active" || a.Status == "Paused") : status == statusList[2] ? shopList.Where(a => a.Status == "Not Active") : new List<Shop>();
            var trimmedSearchQuery = searchQuery.Trim();
            var regex = new Regex(@"^(?=.*\b" + trimmedSearchQuery + @"\b).*$");

            return searchQuery.Length > 0 ? correctStatusShopList.Where(i => regex.IsMatch(i.Name)
                || regex.IsMatch(i.TypeOfShop)
                || regex.IsMatch(i.City)).ToList() : correctStatusShopList.ToList();
        }
    }
}
