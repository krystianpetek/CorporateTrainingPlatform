using GarageGenius.Modules.Reservations.Core.ReservationHistories.Entities;
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
            .IsRequired();

        builder.Property(builder => builder.ReservationId)
            .IsRequired();

        // TODO configure other properties
    }
}
