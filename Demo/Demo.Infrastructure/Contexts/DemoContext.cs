using Demo.Domain;
using Demo.Infrastructure.Seeding;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Demo.Infrastructure.Contexts
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options) : base(options)
        {

        }
        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // Seed data
            modelBuilder.Entity<Country>().Seed();
            modelBuilder.Entity<City>().Seed();
        }
    }
}
