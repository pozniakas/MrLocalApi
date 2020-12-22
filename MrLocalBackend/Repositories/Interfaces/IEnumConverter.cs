using MrLocalDb.Entities;

namespace MrLocalBackend.Repositories.Interfaces
{
    public interface IEnumConverter
    {
        public PriceTypes StringToPricetype(string pricetype);
    }
}
