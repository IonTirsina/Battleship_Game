using Battleship.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Battleship.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(
                options => options.UseSqlServer(connectionString));
        }
    }
}
