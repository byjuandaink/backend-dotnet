using BackendDotNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendDotNet.Infrastructure.Persistence.Configurations;

public class ShipperConfiguration : IEntityTypeConfiguration<Shipper>
{
    public void Configure(EntityTypeBuilder<Shipper> builder)
    {
        builder.ToTable("shippers");
        builder.HasKey(s => s.ShipperId);
        builder.Property(s => s.CompanyName).IsRequired().HasMaxLength(40);
        builder.Property(s => s.Phone).HasMaxLength(24);
    }
}