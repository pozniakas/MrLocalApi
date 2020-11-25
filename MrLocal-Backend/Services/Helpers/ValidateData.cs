using MrLocal_Backend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MrLocal_Backend.Services.Helpers
{
    public class ValidateData
    {
        private readonly ShopRepository shopRepository;
        private readonly ProductRepository productRepository;

        public ValidateData()
        {
            shopRepository = new ShopRepository();
            productRepository = new ProductRepository();
        }
        public bool ValidateProductData(string shopId, string name, string description, double? price, bool isUpdate, string priceType, string id = null)
        {
            var nameRegex = new Regex(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$");
            var priceRegex = new Regex(@"\d+(?:\.\d+)?");
            var shops = shopRepository.FindOne(shopId);
            var priceTypes = new List<string> { "UNIT", "GRAMS", "KILOGRAMS" };

            var doesProductExist = (id != null) && (productRepository.FindOne(id) != null) || id == null;
            var isValidShop = shops != null;
            var isValidName = (isUpdate && name == null) || (name.Length > 2 && nameRegex.IsMatch(name));
            var isValidDescription = (isUpdate && description == null) || (description.Length > 2);
            var isValidPrice = priceRegex.IsMatch(price.ToString()) || (isUpdate && price == null);
            var isValidPriceType = priceTypes.Contains(priceType) || (isUpdate && priceType == null);

            return isValidName && isValidDescription && isValidPrice && isValidShop && doesProductExist && isValidPriceType;
        }

        public async Task<bool> ValidateShopData(string name, string status, string description, string typeOfShop, string city, bool isUpdate)
        {
            string[] arrayOfShopTypes = { "Berries", "Seafood", "Forest food", "Handmade", "Other" };
            string[] arrayOfCities = { "Vilnius", "Kaunas", "Klaipėda", "Šiauliai", "Panevėžys" };
            string[] arrayOfStatusTypes = { "Active", "Not Active", "Paused" };

            var nameRegex = new Regex(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$");
            var shops = await shopRepository.FindAll();

            var isValidName = (isUpdate && name == null) || (name.Length > 2 && nameRegex.IsMatch(name) && shops.Where(i => i.Name == name).Count() == 0);
            var isValidStatus = (isUpdate && status == null) || Array.Exists(arrayOfStatusTypes, i => i == status) || (!isUpdate && status == null);
            var isValidDescription = (isUpdate && description == null) || (description.Length > 2);
            var isValidTypeOfShop = (isUpdate && typeOfShop == null) || Array.Exists(arrayOfShopTypes, i => i == typeOfShop);
            var isValidCity = (isUpdate && city == null) || Array.Exists(arrayOfCities, i => i == city);

            return isValidName && isValidStatus && isValidTypeOfShop && isValidCity && isValidDescription;
        }

        public bool ValidateFilters(ShopRepository shop, string city, string typeOfShop)
        {
            return (city != "All cities" && typeOfShop != "All types" && shop.City == city && shop.TypeOfShop == typeOfShop)
                || (city != "All cities" && typeOfShop == "All types" && shop.City == city)
                || (city == "All cities" && typeOfShop != "All types" && shop.TypeOfShop == typeOfShop)
                || (city == "All cities" && typeOfShop == "All types");
        }
    }
}
