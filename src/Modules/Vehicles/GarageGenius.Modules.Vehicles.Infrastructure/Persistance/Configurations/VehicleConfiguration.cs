using GarageGenius.Modules.Vehicles.Core.Entities;
using GarageGenius.Modules.Vehicles.Core.Types;
using GarageGenius.Modules.Vehicles.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarageGenius.Modules.Vehicles.Infrastructure.Persistance.Configurations;
internal class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(x => x.VehicleId);
        builder.HasIndex(x => x.VehicleId);
        builder.Property(x => x.VehicleId)
            .IsRequired()
            .HasConversion(conversion => conversion.Value, value => new VehicleId(value));

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

        builder.Property(x => x.LicensePlate)
            .IsRequired()
            .HasMaxLength(8)
            .HasConversion(conversion => conversion.Value, value => new LicensePlate(value));

        builder.HasIndex(x => x.Vin)
            .IsUnique();
        builder.Property(x => x.Vin)
            .HasMaxLength(17)
            .HasConversion(conversion => conversion.Value, value => value != default ? new Vin(value) : null);

        builder.Property(x => x.Year)
            .HasMaxLength(8)
            .HasConversion(conversion => conversion.Value, value => value != default ? new Year(value) : null);
    }
}
