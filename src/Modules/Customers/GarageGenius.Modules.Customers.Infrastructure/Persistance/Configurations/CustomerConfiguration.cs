using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Types;
using GarageGenius.Modules.Customers.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarageGenius.Modules.Customers.Infrastructure.Persistance.Configurations;
internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasIndex(x => x.EmailAddress).IsUnique();
        builder.Property(x => x.EmailAddress)
            .IsRequired()
            .HasMaxLength(100)
            .HasConversion(x => x.Value, x => new EmailAddress(x));

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(12)
            .HasConversion(x => x.Value, x => new PhoneNumber(x));

        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, x => new UserId(x));

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new CustomerId(x));

        builder.Property(x => x.FirstName).HasMaxLength(30);
        builder.Property(x => x.LastName).HasMaxLength(50);
    }
}
