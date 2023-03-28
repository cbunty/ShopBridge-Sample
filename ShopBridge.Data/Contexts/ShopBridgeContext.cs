using Microsoft.EntityFrameworkCore;
using ShopBridge.Domain;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;
using ShopBridge.Domain.Enumerations;
using System.Collections.Generic;

namespace ShopBridge.Data.Contexts
{
    public class ShopBridgeContext : DbContext
    {
        public ShopBridgeContext(DbContextOptions options)
            : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is Audit);
            entities.ToList().ForEach(entity =>
            {
                var baseEntity = ((Audit)entity.Entity);
                var now = DateTime.UtcNow;
                if (entity.State == EntityState.Added)
                {
                    baseEntity.CreatedOn = now;
                    baseEntity.CreatedBy = string.IsNullOrWhiteSpace(baseEntity.UserId) ? baseEntity.CreatedBy : baseEntity.UserId;
                }

                baseEntity.ModifiedBy = string.IsNullOrWhiteSpace(baseEntity.UserId) ? baseEntity.ModifiedBy : baseEntity.UserId;
                baseEntity.ModifiedOn = now;
            });

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(GetStatuses());
        }
        private static List<Status> GetStatuses()
        {
            List<Status> statuses = new List<Status>();
            foreach (StatusEnum status in Enum.GetValues(typeof(StatusEnum)))
            {
                statuses.Add(new Status { Id = (byte)status, Name = status.ToString() });
            }
            return statuses;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Status> Status { get; set; }
    }
}
