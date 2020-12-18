using Microsoft.EntityFrameworkCore;
using MrLocalDb.Entities;

namespace MrLocalDb
{
    public class MrLocalDbContext : DbContext
    {
        public MrLocalDbContext(DbContextOptions<MrLocalDbContext> options) : base(options)
        {
        }

        public DbSet<Shop> Shops { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
