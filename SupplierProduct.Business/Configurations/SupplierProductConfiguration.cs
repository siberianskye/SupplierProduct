using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplierProductExercise.Business.Entities;

namespace SupplierProductExercise.Business.Configurations
{
    public class SupplierProductConfiguration : IEntityTypeConfiguration<SupplierProduct>
    {
        public void Configure(EntityTypeBuilder<SupplierProduct> builder)
        {
            builder.ToTable("SupplierProduct", "SupplierProductExercise");

            builder.Property(supplierProduct => supplierProduct.SKU)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(supplierProduct => supplierProduct.Name)
                .HasMaxLength(200);

            builder.HasIndex(supplierProduct => new { supplierProduct.SKU }).IsUnique();
        }
    }
}
