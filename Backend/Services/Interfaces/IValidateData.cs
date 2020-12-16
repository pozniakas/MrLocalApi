using Backend.Models;
using System.Threading.Tasks;

namespace Backend.Services.Interfaces
{
    public interface IValidateData
    {
        public Task<bool> ValidateProductData(string shopId, string name, string description, double? price, bool isUpdate, string priceType);
        public Task<bool> ValidateShopData(string name, string status, string description, string typeOfShop, string city, bool isUpdate);
        public bool ValidateFilters(Shop shop, string city, string typeOfShop);
    }
}
