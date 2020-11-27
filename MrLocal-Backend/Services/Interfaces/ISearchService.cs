using MrLocal_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrLocal_Backend.Services.Interfaces
{
    interface ISearchService
    {
        public Task<List<ShopModel>> SearchForShops(string searchQuery, string city = "All cities", string typeOfShop = "All types");
    }
}
