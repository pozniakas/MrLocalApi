using MrLocalDb.Entities;
using System.Threading.Tasks;

namespace MrLocalBackend.Repositories.Interfaces
{
    public interface ILocationRepository
    {
        Task<Shop> Create(string name, string description, string typeOfShop, string latitude, string longitude, string city);
    }
}
