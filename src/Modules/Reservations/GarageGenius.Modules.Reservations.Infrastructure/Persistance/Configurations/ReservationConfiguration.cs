using GarageGenius.Modules.Reservations.Core.Reservations.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarageGenius.Modules.Reservations.Infrastructure.Persistance.Configurations;
internal class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(builder => builder.ReservationId);
        builder.HasIndex(builder => builder.VehicleId);

        builder.Property(builder => builder.ReservationId)
            .IsRequired();

        builder.Property(builder => builder.VehicleId)
            .IsRequired();

        // TODO configure other properties
    }
}
