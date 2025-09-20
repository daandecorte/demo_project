using Demo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Configuration
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("tblCities", "City").HasKey(p => p.Id);
            builder.HasIndex(c => c.Id).IsUnique();
            builder.Property(c => c.Id).HasColumnType("int");
            builder.Property(c => c.Population).HasColumnType("bigint");//.HasAnnotation("Range", new RangeAttribute(0, 10000000000));
            builder.Property(c => c.Name).IsRequired().HasColumnType("nvarchar(50)");
        }
    }
}
