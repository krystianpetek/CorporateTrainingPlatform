using GarageGenius.Modules.Reservations.Core.ReservationHistories.Entities;
using GarageGenius.Modules.Reservations.Core.ReservationHistories.Types;
using GarageGenius.Modules.Reservations.Core.ReservationHistories.ValueObjects;
using GarageGenius.Modules.Reservations.Core.Reservations.Types;
using GarageGenius.Modules.Reservations.Core.Reservations.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarageGenius.Modules.Reservations.Infrastructure.Persistance.Configurations;
internal class ReservationHistoryConfiguration : IEntityTypeConfiguration<ReservationHistory>
{
    public void Configure(EntityTypeBuilder<ReservationHistory> builder)
    {
        builder.HasKey(builder => builder.ReservationHistoryId);
        builder.HasIndex(builder => builder.ReservationId);

        builder.Property(builder => builder.ReservationHistoryId)
            .IsRequired()
            .HasConversion(conversion => conversion.Value, value => new ReservationHistoryId(value));

        builder.Property(builder => builder.ReservationId)
            .IsRequired()
            .HasConversion(conversion => conversion.Value, value => new ReservationId(value));

        builder.Property(builder => builder.ReservationState)
            .IsRequired()
            .HasConversion(conversion => conversion.Value, value => new ReservationState(value));

        builder.Property(builder => builder.Comment)
            .IsRequired()
            .HasConversion(conversion => conversion.Value, value => new Comment(value));
    }
}
