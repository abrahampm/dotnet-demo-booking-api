using alten_test.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace alten_test.DataAccessLayer.Context
{
    public class DatabaseContextFactory : IDatabaseContextFactory
    {
        private readonly DatabaseContext _context;

        public DatabaseContextFactory(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseMySQL(configuration.GetConnectionString("MySqlServerConnection"));
            _context = new DatabaseContext(optionsBuilder.Options);
        }

        public DatabaseContext GetContext()
        {
            return _context;
        }
    }
}