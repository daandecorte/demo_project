using Demo.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Infrastructure.Seeding
{
    public static class CitySeeding
    {
        public static void Seed(this EntityTypeBuilder<City> modelBuilder)
        {
            modelBuilder.HasData(
                new City { Id = -1, Name = "Antwerpen", Population = 10000000, CountryId = -1 }
            );
        }
    }
}
