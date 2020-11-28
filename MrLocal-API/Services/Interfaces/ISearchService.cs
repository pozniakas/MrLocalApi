using MrLocal_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrLocal_API.Services.Interfaces
{
    interface ISearchService
    {
        public Task<List<Shop>> SearchForShops(string searchQuery, string city = "All cities", string typeOfShop = "All types");
    }
}
