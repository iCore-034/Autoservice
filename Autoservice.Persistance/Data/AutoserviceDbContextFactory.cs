using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Text.Json;

namespace Autoservice.Persistance.Data
{
    public class AutoserviceDbContextFactory : IDesignTimeDbContextFactory<AutoserviceDbContext>
    {
        public AutoserviceDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<AutoserviceDbContext>();
            //options.UseNpgsql("Host=localhost;Port=5432;Database=Autoservice;Username=postgres;Password=qwertyfsy");
            //return new AutoserviceDbContext(options.Options);

            string cs = GetConnectionStringFromFile();
            options.UseNpgsql(cs);
            return new AutoserviceDbContext(options.Options);
        }
        private string GetConnectionStringFromFile()
        {
            string path = @"Settings\ConnectionString.json";
            Connection connection = JsonSerializer.Deserialize<Connection>(File.ReadAllText(path));
            return $"Host={connection.Host};Port={connection.Port};Database={connection.Database};Username={connection.Username};Password={connection.Password}";
        }
    }
}
