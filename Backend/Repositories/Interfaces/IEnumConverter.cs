using static Backend.Models.Product;

namespace Backend.Repositories.Interfaces
{
    public interface IEnumConverter
    {
        public PriceTypes StringToPricetype(string pricetype);
    }
}
