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
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion(x => x.Value, x => new CustomerId(x));

        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, x => new UserId(x));

        builder.Property(x => x.FirstName)
            .HasMaxLength(50)
            .HasConversion(x => x.Value, x => new FirstName(x));

        builder.Property(x => x.LastName)
            .HasMaxLength(50)
            .HasConversion(x => x.Value, x => new LastName(x));

        builder.Property(x => x.EmailAddress)
            .IsRequired()
            .HasMaxLength(100)
            .HasConversion(x => x.Value, x => new EmailAddress(x));

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(12)
            .HasConversion(x => x.Value, x => new PhoneNumber(x));
    }
}
