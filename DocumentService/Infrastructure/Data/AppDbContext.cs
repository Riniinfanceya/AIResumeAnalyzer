using Microsoft.EntityFrameworkCore;
using DocumentService.Core.Entities;

namespace DocumentService.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }

        public override int SaveChanges()
        {
            ApplyAuditInfo();

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            ApplyAuditInfo();

            return await base.SaveChangesAsync(
                cancellationToken);
        }

        private void ApplyAuditInfo()
        {
            foreach (var entry in ChangeTracker
                         .Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt =
                        DateTime.UtcNow;

                    entry.Entity.IsActive =
                        true;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt =
                        DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.State =
                        EntityState.Modified;

                    entry.Entity.IsActive =
                        false;

                    entry.Entity.UpdatedAt =
                        DateTime.UtcNow;
                }
            }
        }
    }
}