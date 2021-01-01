using MrLocalDb.Entities;
using System.Threading.Tasks;

namespace MrLocalBackend.Repositories.Interfaces
{
    public interface ILocationRepository
    {
        Task<Location> Create(string latitude, string longitude, string shopId);
    }
}
