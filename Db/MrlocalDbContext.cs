using Db.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Db
{
    public class MrlocalDbContext : DbContext
    {
        public MrlocalDbContext(DbContextOptions<MrlocalDbContext> options) : base(options)
        {
        }

        public DbSet<Shop> Shops { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
