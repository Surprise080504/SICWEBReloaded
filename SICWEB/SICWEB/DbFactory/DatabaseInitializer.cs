using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SICWEB.Models2;
using System.Threading.Tasks;

namespace SICWEB.DbFactory
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }

    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly SICDBWEB_MYSContext _context;

        public DatabaseInitializer(SICDBWEB_MYSContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync().ConfigureAwait(false);
        }
    }
}