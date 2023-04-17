using Autoservice.Persistance.Models;
using Microsoft.EntityFrameworkCore;

namespace Autoservice.Persistance.Data
{
    public class AutoserviceDbContext : DbContext
    {
        public AutoserviceDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Car> Car { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<ServiceOrder> ServiceOrder { get; set; }
    }
}
