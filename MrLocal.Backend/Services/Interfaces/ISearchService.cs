using MrLocal.Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrLocal.Backend.Services.Interfaces
{
    public interface ISearchService
    {
        public Task<List<Shop>> SearchForShops(string searchQuery, string city = "All cities", string typeOfShop = "All types");
    }
}
