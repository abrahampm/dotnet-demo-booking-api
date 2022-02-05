using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace alten_test.DataAccessLayer.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var appSettingsPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(),
                "../alten-test.PresentationLayer/")); 
            var builder = new ConfigurationBuilder()
                .SetBasePath(appSettingsPath)
                .AddJsonFile("appsettings.json");

            var connectionString = builder.Build().GetSection("ConnectionStrings").GetSection("MySqlServerConnection")
                .Value;
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseMySQL(connectionString);
            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}