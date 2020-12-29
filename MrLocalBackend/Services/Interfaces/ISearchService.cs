using System.Collections.Generic;
using System.Threading.Tasks;
using MrLocalDb.Entities;

namespace MrLocalBackend.Services.Interfaces
{
    public interface ISearchService
    {
        public Task<List<Shop>> SearchForShops(string searchQuery, string status, string city = "All cities", string typeOfShop = "All types");
    }
}
