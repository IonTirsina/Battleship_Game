using Battleship.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Battleship.Tests.Mocks
{
    internal static class TestApplicationDbContext
    {
        private static ApplicationDbContext _dbContext = Get();
        private static ApplicationDbContext InitTestApplicationDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var databaseContext = new ApplicationDbContext(options);

            databaseContext.Database.EnsureCreated();

            return databaseContext;

        }

        public static ApplicationDbContext Get()
        {
            return _dbContext ??= InitTestApplicationDbContext();
        }

    }
}
