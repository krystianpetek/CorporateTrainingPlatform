using GarageGenius.Modules.Cars.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarageGenius.Modules.Cars.Infrastructure.Persistance.Configurations;
internal class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.HasIndex(x => x.Vin).IsUnique();
        builder.Property(x => x.Vin)
            .HasMaxLength(15);

        builder.Property(x => x.Model)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Manufacturer)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LicensePlate)
            .HasMaxLength(100);

        builder.Property(x => x.CustomerId)
            .IsRequired();
    }
}
