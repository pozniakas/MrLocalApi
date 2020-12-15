using MrLocal_API.Models;
using MrLocal_API.Repositories.Interfaces;
using MrLocal_API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MrLocal_API.Services.Helpers
{
    public class ValidateData : IValidateData
    {
        private readonly IShopRepository _shopRepository;

        public ValidateData(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }
        public async Task<bool> ValidateProductData(string shopId, string name, string description, double? price, bool isUpdate, string priceType)
        {
            static bool IsStringEmpty(string str) => str == null || str.Length == 0;

            var nameRegex = new Regex(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$");
            var priceRegex = new Regex(@"\d+(?:\.\d+)?");
            var shop = await _shopRepository.FindOne(shopId);
            var priceTypes = new List<string> { "UNIT", "GRAMS", "KILOGRAMS" };
            var isValidShop = shop != null;
            var isValidName = (isUpdate && IsStringEmpty(name)) || (name != null && name.Length > 2 && nameRegex.IsMatch(name));
            var isValidDescription = (isUpdate && IsStringEmpty(description)) || (description != null && description.Length > 2);
            var isValidPrice = priceRegex.IsMatch(price.ToString()) || (isUpdate && price == null);
            var isValidPriceType = priceTypes.Contains(priceType) || (isUpdate && IsStringEmpty(priceType));

            bool[] validators = { isValidShop, isValidName, isValidDescription, isValidPrice, isValidPriceType };
            string[] namesOfParams = { "shop", "name", "description", "price", "price type" };

            for (var i = 0; i < validators.Length; i++)
            {
                if (!validators[i])
                {
                    throw new ArgumentException($"Not valid {namesOfParams[i]}");
                }
            }

            return true;
        }

        public async Task<bool> ValidateShopData(string name, string status, string description, string typeOfShop, string city, bool isUpdate)
        {
            static bool IsStringEmpty(string str) => str == null || str.Length == 0;

            string[] arrayOfShopTypes = { "Berries", "Seafood", "Forest food", "Handmade", "Other" };
            string[] arrayOfCities = { "Vilnius", "Kaunas", "Klaipėda", "Šiauliai", "Panevėžys" };
            string[] arrayOfStatusTypes = { "Active", "Not Active", "Paused" };
            var nameRegex = new Regex(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$");
            var shops = await _shopRepository.FindAll();

            var isValidName = (isUpdate && IsStringEmpty(name)) || (name != null && name.Length > 2 && nameRegex.IsMatch(name) && shops.Where(i => i.Name == name).Count() == 0);
            var isValidStatus = (isUpdate && IsStringEmpty(status)) || Array.Exists(arrayOfStatusTypes, i => i == status) || (!isUpdate && IsStringEmpty(status));
            var isValidDescription = (isUpdate && IsStringEmpty(description)) || (description != null && description.Length > 2);
            var isValidTypeOfShop = (isUpdate && IsStringEmpty(description)) || Array.Exists(arrayOfShopTypes, i => i == typeOfShop);
            var isValidCity = (isUpdate && IsStringEmpty(city)) || Array.Exists(arrayOfCities, i => i == city);

            bool[] validators = { isValidName, isValidStatus, isValidDescription, isValidTypeOfShop, isValidCity };
            string[] namesOfParams = { "name", "status", "description", "typeOfShop", "city" };

            for (var i = 0; i < validators.Length; i++)
            {
                if (!validators[i])
                {
                    throw new ArgumentException($"Not valid {namesOfParams[i]}");
                }
            }

            return true;
        }

        public bool ValidateFilters(Shop shop, string city, string typeOfShop)
        {
            return (city != "All cities" && typeOfShop != "All types" && shop.City == city && shop.TypeOfShop == typeOfShop)
                || (city != "All cities" && typeOfShop == "All types" && shop.City == city)
                || (city == "All cities" && typeOfShop != "All types" && shop.TypeOfShop == typeOfShop)
                || (city == "All cities" && typeOfShop == "All types");
        }
    }
}
