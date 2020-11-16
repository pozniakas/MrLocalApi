using MrLocal_Backend.Repositories;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
    }
}
