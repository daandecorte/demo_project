using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AP.MyGameStore.Infrastructure.Contexts
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
