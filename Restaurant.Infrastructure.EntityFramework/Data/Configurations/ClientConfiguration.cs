using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities;
using Restaurant.ValueObjects;

namespace Restaurant.Infrastructure.EntityFramework.Data.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasConversion(name => name.Value, str => new PersonName(str))
            .HasMaxLength(150);

        builder.Property(x => x.Phone)
            .IsRequired()
            .HasConversion(phone => phone.Value, str => new Phone(str))
            .HasMaxLength(20);

        builder.HasMany<Reservation>("_reservations")
            .WithOne(x => x.Client)
            .HasForeignKey("ClientId")
            .HasPrincipalKey(x => x.Id);

        builder.Ignore(x => x.Reservations);
    }
}
