using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DotnetTestingApp.Entities.DB
{
    public class DatabaseContext(
        IConfiguration configuration
    ) : DbContext
    {
        public DbSet<User> User { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public IConfiguration Configuration { get; } = configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = Configuration.GetConnectionString("DatabaseConnection");
            optionsBuilder.UseSqlite(connection);
        }
    }
}
