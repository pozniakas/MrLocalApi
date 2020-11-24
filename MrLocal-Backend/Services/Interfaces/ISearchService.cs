using MrLocal_Backend.Repositories;
using System.Collections.Generic;

namespace MrLocal_Backend.Services.Interfaces
{
    interface ISearchService
    {
        public List<ShopRepository> SearchForShops(string searchQuery, string city = "All cities", string typeOfShop = "All types");
    }
}
