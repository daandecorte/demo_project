using Demo.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Infrastructure.Seeding
{
    public static class CountrySeeding
    {
        public static void Seed(this EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                new Country { Id = -1, Name="België" }
            );
        }
    }
}
