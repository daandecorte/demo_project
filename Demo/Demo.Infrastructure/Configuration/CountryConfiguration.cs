using Demo.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Configuration
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("tblCountries", "Country").HasKey(p => p.Id);
            builder.HasIndex(c => c.Id).IsUnique();
            builder.Property(c => c.Id).HasColumnType("int");
            builder.Property(c => c.Name).IsRequired().HasColumnType("nvarchar(50)");
            builder.HasMany(c => c.Cities).WithOne(c => c.Country);
        }
    }
}
