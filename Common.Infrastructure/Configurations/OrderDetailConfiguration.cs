using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Domain.Entities;
using Products.Domain.Entities;

public sealed class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable("Order Details");

        builder.HasKey(od => new { od.OrderId, od.ProductId });

        builder.HasOne(od => od.Order)
               .WithMany(o => o.OrderDetails)
               .HasForeignKey(od => od.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Product>()
               .WithMany()
               .HasForeignKey(od => od.ProductId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(od => od.UnitPrice)
               .HasColumnType("decimal(18,2)");

        builder.Property(od => od.Discount)
               .HasColumnType("decimal(18,2)");

        builder.Property(od => od.OrderId)
               .HasColumnName("OrderID");

        builder.Property(od => od.ProductId)
               .HasColumnName("ProductID");

        builder.Property(od => od.Quantity)
               .HasColumnName("Quantity")
               .HasColumnType("smallint");
    }
}