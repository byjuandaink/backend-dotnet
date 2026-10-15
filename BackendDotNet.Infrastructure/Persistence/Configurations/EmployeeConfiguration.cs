using BackendDotNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendDotNet.Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employees");
        builder.HasKey(e => e.EmployeeId);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(20);
        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(10);
        builder.Property(e => e.Title).HasMaxLength(30);
        builder.Property(e => e.TitleOfCourtesy).HasMaxLength(25);
        builder.Property(e => e.Address).HasMaxLength(60);
        builder.Property(e => e.City).HasMaxLength(15);
        builder.Property(e => e.Region).HasMaxLength(15);
        builder.Property(e => e.PostalCode).HasMaxLength(10);
        builder.Property(e => e.Country).HasMaxLength(15);
        builder.Property(e => e.HomePhone).HasMaxLength(24);
        builder.Property(e => e.Extension).HasMaxLength(4);
        builder.HasIndex(e => e.LastName);
        builder.HasIndex(e => e.PostalCode);

        builder.HasOne(e => e.Manager)
            .WithMany(e => e.Subordinates)
            .HasForeignKey(e => e.ReportsTo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}