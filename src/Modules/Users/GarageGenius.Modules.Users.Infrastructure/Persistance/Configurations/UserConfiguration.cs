using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Modules.Users.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarageGenius.Modules.Users.Infrastructure.Persistance.Configurations;
internal class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasKey(key => key.UserId);
		builder.HasIndex(index => index.Email).IsUnique();

		builder.Property(property => property.UserId)
			.IsRequired();

		builder.Property(property => property.CustomerId)
			.IsRequired();

		builder.Property(property => property.RoleName)
			.IsRequired();

		builder.Property(property => property.State)
			.IsRequired()
			.HasConversion(state => state.Value, state => new UserState(state));

		builder.Property(property => property.Email)
			.IsRequired()
			.HasMaxLength(100)
			.HasConversion(email => email.Value, newEmail => new EmailAddress(newEmail));

		builder.Property(property => property.Password)
			.IsRequired()
			.HasMaxLength(100);
	}
}
