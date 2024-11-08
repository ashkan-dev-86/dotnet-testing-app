using Microsoft.EntityFrameworkCore;

namespace DotnetTestingApp.Entities.DB
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public List<User> Users { get; set; } = new List<User>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DotNetTestingDb.db");
        }
    }
}
