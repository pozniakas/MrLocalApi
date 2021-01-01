using MrLocalBackend.Repositories.Interfaces;
using MrLocalDb;
using MrLocalDb.Entities;
using System;
using System.Threading.Tasks;

namespace MrLocalBackend.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly MrLocalDbContext _context;

        public LocationRepository(MrLocalDbContext context)
        {
            _context = context;
        }

        public async Task<Location> Create(string latitude, string longitude)
        {
            var updatedAt = DateTime.UtcNow;
            var createdAt = DateTime.UtcNow;
            var locationId = Guid.NewGuid().ToString();

            var location = new Location(locationId, latitude, longitude, createdAt, updatedAt);
            _context.Location.Add(location);
            await _context.SaveChangesAsync();

            return location;
        }
    }
}
