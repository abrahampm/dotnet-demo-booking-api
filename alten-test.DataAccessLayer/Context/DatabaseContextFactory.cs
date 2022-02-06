using alten_test.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace alten_test.DataAccessLayer.Context
{
    public class DatabaseContextFactory : IDatabaseContextFactory
    {
        private readonly ApplicationDbContext _context;

        public DatabaseContextFactory(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySQL(configuration.GetConnectionString("MySqlServerConnection"));
            _context = new ApplicationDbContext(optionsBuilder.Options);
        }

        public ApplicationDbContext GetContext()
        {
            return _context;
        }
    }
}