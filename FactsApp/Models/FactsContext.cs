using Microsoft.EntityFrameworkCore;

namespace FactsApp.Models
{
    public class FactsContext : DbContext
    {
        public DbSet<Fact> Facts { get; set; }
        public FactsContext(DbContextOptions<FactsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=password");
        }
    }
}
