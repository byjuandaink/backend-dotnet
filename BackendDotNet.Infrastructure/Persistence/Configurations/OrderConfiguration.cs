using BackendDotNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendDotNet.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");
        builder.HasKey(o => o.OrderId);
        builder.Property(o => o.Freight).HasPrecision(18, 2);
        builder.Property(o => o.ShipName).HasMaxLength(40);
        builder.Property(o => o.ShipAddress).HasMaxLength(60);
        builder.Property(o => o.ShipCity).HasMaxLength(15);
        builder.Property(o => o.ShipRegion).HasMaxLength(15);
        builder.Property(o => o.ShipPostalCode).HasMaxLength(10);
        builder.Property(o => o.ShipCountry).HasMaxLength(15);
        builder.HasIndex(o => o.OrderDate);
        builder.HasIndex(o => o.ShippedDate);
        builder.HasIndex(o => o.ShipPostalCode);

        builder.HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Employee)
            .WithMany(e => e.Orders)
            .HasForeignKey(o => o.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Shipper)
            .WithMany(s => s.Orders)
            .HasForeignKey(o => o.ShipVia)
            .OnDelete(DeleteBehavior.Restrict);
    }
}