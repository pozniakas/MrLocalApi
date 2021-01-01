using MrLocalBackend.Repositories.Interfaces;
using MrLocalBackend.Services.Interfaces;
using MrLocalDb;
using MrLocalDb.Entities;
using System;
using System.Threading.Tasks;

namespace MrLocalBackend.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly MrLocalDbContext _context;
        private readonly IShopService _shopService;
        public LocationRepository(MrLocalDbContext context, IShopService shopService)
        {
            _context = context;
            _shopService = shopService;
        }

        public async Task<Shop> Create(string name, string description, string typeOfShop, string latitude, string longitude, string city)
        {
            var updatedAt = DateTime.UtcNow;
            var createdAt = DateTime.UtcNow;
            var locationId = Guid.NewGuid().ToString();

            var location = new Location(locationId, latitude, longitude, createdAt, updatedAt);
            _context.Location.Add(location);
            await _context.SaveChangesAsync();

            var createdShop = await _shopService.CreateShop(name, description, typeOfShop, city, locationId);

            return createdShop;
        }
    }
}
