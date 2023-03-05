using Battleship.Persistence.Interfaces;
using Battleship.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Battleship.Persistence.EntityDbConfigurations;

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
    }
}
