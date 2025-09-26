using Demo.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Infrastructure.Seeding
{
    public static class CitySeeding
    {
        public static void Seed(this EntityTypeBuilder<City> modelBuilder)
        {
            modelBuilder.HasData(
                new City { Id = -1, Name = "Antwerpen", Population = 10000000, CountryId = 1 },
                new City { Id = -2, Name = "Brussel", Population = 20000000, CountryId = 1 },
                new City { Id = -3, Name = "Gent", Population = 5000000, CountryId = 1 },
                new City { Id = -4, Name = "Brugge", Population = 2000000, CountryId = 1 }
            );
        }
    }
}
