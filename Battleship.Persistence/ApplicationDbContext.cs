using Battleship.Persistence.Interfaces;
using Battleship.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Battleship.Persistence.EntityDbConfigurations;
using Battleship.Domain.Common;

namespace Battleship.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityDbConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        // TODO : soft delete and hard delete
        private void AuditBaseEntities()
        {
            var entries = ChangeTracker.Entries().Where((e) => e.Entity is BaseEntity && (e.State == EntityState.Modified || e.State == EntityState.Added));

            foreach(var entry in entries)
            {
                var now = DateTime.UtcNow;

                if(entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).DateCreated = now;
                }

                if(entry.State == EntityState.Modified)
                {
                    ((BaseEntity)entry.Entity).DateUpdated = now;
                }
            }
        }

        public override int SaveChanges()
        {
            AuditBaseEntities();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AuditBaseEntities();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
