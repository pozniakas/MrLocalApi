using static MrLocal.Backend.Models.Product;

namespace MrLocal.Backend.Repositories.Interfaces
{
    public interface IEnumConverter
    {
        public PriceTypes StringToPricetype(string pricetype);
    }
}
