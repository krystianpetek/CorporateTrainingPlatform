using GarageGenius.Modules.Cars.Core.Entities;
using GarageGenius.Modules.Cars.Core.Types;
using GarageGenius.Modules.Cars.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarageGenius.Modules.Cars.Infrastructure.Persistance.Configurations;
internal class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.HasIndex(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion(conversion => conversion.Value, value => new CarId(value));

        builder.Property(x => x.CustomerId)
            .IsRequired()
            .HasConversion(conversion => conversion.Value, value => new CustomerId(value));

        builder.Property(x => x.Manufacturer)
            .IsRequired()
            .HasMaxLength(100)
            .HasConversion(conversion => conversion.Value, value => new Manufacturer(value));

        builder.Property(x => x.Model)
            .IsRequired()
            .HasMaxLength(100)
            .HasConversion(conversion => conversion.Value, value => new Model(value));

        builder.HasIndex(x => x.Vin)
            .IsUnique();
        builder.Property(x => x.Vin)
            .HasMaxLength(17)
            .HasConversion(conversion => conversion.Value, value => new Vin(value));

        builder.Property(x => x.LicensePlate)
            .HasMaxLength(8)
            .HasConversion(conversion => conversion.Value, value => new LicensePlate(value));

        builder.Property(x => x.Year)
            .HasMaxLength(8)
            .HasConversion(conversion => conversion.Value, value => new Year(value));
    }
}
