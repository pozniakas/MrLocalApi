namespace MrLocal_Backend.Services.Interfaces
{
    interface IProductService
    {
        public void AddProductToShop(string shopId, string name, string description, string priceType, double? price);
        public void UpdateProduct(string id, string shopId, string name, string description, string priceType, double? price);
        public void DeleteProduct(string id);
    }
}
