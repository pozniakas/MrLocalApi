using static MrLocal_API.Models.Product;

namespace MrLocal_API.Repositories.Interfaces
{
    public interface IEnumConverter
    {
        public PriceTypes StringToPricetype(string pricetype);
    }
}
