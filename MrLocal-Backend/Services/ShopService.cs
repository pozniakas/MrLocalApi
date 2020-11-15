using MrLocal_Backend.Repositories;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MrLocal_Backend.Services
{
    public class ShopService
    {
        private readonly ShopRepository shopRepository;
        public ShopService()
        {
            shopRepository = new ShopRepository();
        }

        public void CreateShop(string name, string description, string typeOfShop, string city)
        {
            if (ValidateShopData(name, null, description, typeOfShop, city, false))
            {
                shopRepository.Create(name, description, typeOfShop, city);
            }
            else
            {
                throw new ArgumentException("Invalid shop parameters for creation");
            }
        }

        public void UpdateShop(string id, string name, string status, string description, string typeOfShop, string city)
        {
            if (!ValidateShopData(name, status, description, typeOfShop, city, true))
            {
                throw new ArgumentException("Invalid shop parameters for update");
            }

            shopRepository.Update(id, name, status, description, typeOfShop, city);
        }

        public void DeleteShop(string id)
        {
            var shop = shopRepository.FindOne(id);

            if (shop != null)
            {
                shopRepository.Delete(id);
            }
            else
            {
                throw new ArgumentException("Invalid id for deleting shop");
            }
        }

        public ShopRepository GetShop(string id)
        {
            var shop = shopRepository.FindOne(id);

            if (shop == null)
            {
                throw new ArgumentException("Invalid id for getting shop");
            }

            return shop;
        }

        private bool ValidateShopData(string name, string status, string description, string typeOfShop, string city, bool isUpdate)
        {
            string[] arrayOfShopTypes = { "Berries", "Seafood", "Forest food", "Handmade", "Other" };
            string[] arrayOfCities = { "Vilnius", "Kaunas", "Klaipėda", "Šiauliai", "Panevėžys" };
            string[] arrayOfStatusTypes = { "Active", "Not Active", "Paused" };

            var nameRegex = new Regex(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$");
            var shops = shopRepository.FindAll();

            var isValidName = (isUpdate && name == "") || (name.Length > 2 && nameRegex.IsMatch(name) && shops.Where(i => i.Name == name).Count() == 0);
            var isValidStatus = (isUpdate && status == "") || Array.Exists(arrayOfStatusTypes, i => i == status) || (!isUpdate && status == null);
            var isValidDescription = (isUpdate && description == "") || (description.Length > 2);
            var isValidTypeOfShop = (isUpdate && typeOfShop == "") || Array.Exists(arrayOfShopTypes, i => i == typeOfShop);
            var isValidCity = (isUpdate && city == "") || Array.Exists(arrayOfCities, i => i == city);

            return isValidName && isValidStatus && isValidTypeOfShop && isValidCity && isValidDescription;
        }
    }
}
