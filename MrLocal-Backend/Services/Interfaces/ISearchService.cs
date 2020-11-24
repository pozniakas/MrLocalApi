using MrLocal_Backend.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrLocal_Backend.Services.Interfaces
{
    interface ISearchService
    {
        public Task<List<ShopRepository>> SearchForShops(string searchQuery, string city = "All cities", string typeOfShop = "All types");
    }
}
