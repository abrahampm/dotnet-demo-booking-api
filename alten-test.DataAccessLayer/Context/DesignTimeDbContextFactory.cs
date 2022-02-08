using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace alten_test.DataAccessLayer.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var appSettingsPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(),
                "../alten-test.PresentationLayer/")); 
            var builder = new ConfigurationBuilder()
                .SetBasePath(appSettingsPath)
                .AddJsonFile("appsettings.json");

            var connectionString = builder.Build().GetSection("ConnectionStrings").GetSection("MySqlServerConnection")
                .Value;
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySQL(connectionString);
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}