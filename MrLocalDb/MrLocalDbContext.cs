using System;
using System.Threading;
using System.Threading.Tasks;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Shop>().Property<DateTime?>("DeletedAt");
            builder.Entity<Shop>().HasQueryFilter(m => EF.Property<DateTime?>(m, "DeletedAt") == null);
            builder.Entity<Product>().Property<DateTime?>("DeletedAt");
            builder.Entity<Product>().HasQueryFilter(m => EF.Property<DateTime?>(m, "DeletedAt") == null);
        }
        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["DeletedAt"] = null;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["DeletedAt"] = DateTime.UtcNow;
                        break;
                }
            }
        }
    }
}
