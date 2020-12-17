using static MrLocalBackend.Models.Product;

namespace MrLocalBackend.Repositories.Interfaces
{
    public interface IEnumConverter
    {
        public PriceTypes StringToPricetype(string pricetype);
    }
}
