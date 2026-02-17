using Domain;
using Domain.common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class PASDbContext : DbContext
    {
        public readonly DbContextOptions<PASDbContext> _context;
        

        public PASDbContext(DbContextOptions<PASDbContext> options) : base(options)
        {
            _context = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseDomainEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(BaseDomainEntity.CreatedDate))
                        .HasDefaultValue(DateTime.UtcNow);

                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(BaseDomainEntity.UpdatedDate))
                        .HasDefaultValue(DateTime.UtcNow);
                }
            }
        }


        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries<BaseDomainEntity>())
            {
                entity.Entity.UpdatedDate = DateTime.Now;
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedDate = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }



    }


}