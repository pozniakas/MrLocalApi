using MrLocalDb.Entities;
using Microsoft.EntityFrameworkCore;
using System;

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
