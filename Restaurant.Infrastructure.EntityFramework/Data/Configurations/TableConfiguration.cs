using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities;

namespace Restaurant.Infrastructure.EntityFramework.Data.Configurations;

public class TableConfiguration : IEntityTypeConfiguration<Table>
{
    public void Configure(EntityTypeBuilder<Table> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired().ValueGeneratedNever();

        builder.Property(x => x.Number).IsRequired();
        builder.Property(x => x.Seats).IsRequired();
        builder.Property(x => x.IsAvailable).IsRequired();
    }
}
