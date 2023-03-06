using Microsoft.EntityFrameworkCore;
using Battleship.Domain.Entities;

namespace Battleship.Persistence.Interfaces
{
    public interface IApplicationDbContext : IDisposable
    {
        DbSet<User> Users { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
