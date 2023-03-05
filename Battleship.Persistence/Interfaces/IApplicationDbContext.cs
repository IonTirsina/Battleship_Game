using Microsoft.EntityFrameworkCore;
using Battleship.Domain.Entities;

namespace Battleship.Persistence.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
