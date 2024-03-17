using GarageGenius.Modules.Notifications.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarageGenius.Modules.Notifications.Infrastructure.Persistence.Configurations;
internal class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
	public void Configure(EntityTypeBuilder<Notification> builder)
	{
		builder.HasIndex(x => x.Id).IsUnique();
		builder.Property(x => x.Id)
			.IsRequired();

		builder.Property(x => x.UserId)
			.IsRequired();

		builder.Property(x => x.Message)
			.HasMaxLength(50);

		builder.Property(x => x.Status)
			.HasMaxLength(20)
			.HasConversion(
				v => v.ToString(),
				v => (NotificationStatus)Enum.Parse(typeof(NotificationStatus), v));
	}
}
