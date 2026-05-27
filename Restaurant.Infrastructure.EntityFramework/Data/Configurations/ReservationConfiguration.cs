using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;

namespace Restaurant.Infrastructure.EntityFramework.Data.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(x => x.GuestsCount).IsRequired();
        builder.Property(x => x.ConfirmedBy).IsRequired(false);

        builder.Property(x => x.StartTime).IsRequired().HasConversion(
            src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
            dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc));

        builder.Property(x => x.EndTime).IsRequired().HasConversion(
            src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
            dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc));

        builder.HasOne(x => x.Client).WithMany("_reservations");
        builder.HasOne(x => x.Table).WithMany();

        builder.HasOne<Admin>()
            .WithMany()
            .HasForeignKey(x => x.ConfirmedBy)
            .IsRequired(false);
    }
}
