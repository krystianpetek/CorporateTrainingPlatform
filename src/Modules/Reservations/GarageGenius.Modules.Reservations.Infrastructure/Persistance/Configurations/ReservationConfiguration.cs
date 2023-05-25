using GarageGenius.Modules.Reservations.Core.Reservations.Entities;
using GarageGenius.Modules.Reservations.Core.Reservations.Types;
using GarageGenius.Modules.Reservations.Core.Reservations.ValueObjects;
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
            .IsRequired()
            .HasConversion(conversion => conversion.Value, value => new ReservationId(value));

        builder.Property(builder => builder.VehicleId)
            .HasConversion(conversion => conversion.Value, value => new VehicleId(value))
            .IsRequired();

        builder.Property(x => x.ReservationDate)
            .IsRequired()
            .HasConversion(conversion => conversion.Value, value => value != default ? new ReservationDate(value) : null);

        builder.Property(x => x.ReservationState)
            .IsRequired()
            .HasConversion(conversion => conversion.Value, value => value != default ? new ReservationState(value) : null);

        builder.Property(x => x.ReservationNote)
            .IsRequired()
            .HasConversion(conversion => conversion.Value, value => value != default ? new ReservationNote(value) : null);

        builder.Property(x => x.ReservationDeleted)
            .IsRequired();
    }
}
