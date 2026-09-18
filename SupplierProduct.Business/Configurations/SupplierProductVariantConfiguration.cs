using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplierProductExercise.Business.Entities;

namespace SupplierProductExercise.Business.Configurations
{
    public class SupplierProductVariantConfiguration : IEntityTypeConfiguration<SupplierProductVariant>
    {
        public void Configure(EntityTypeBuilder<SupplierProductVariant> builder)
        {
            builder.ToTable("SupplierProductVariant", "SupplierProductExercise");

            builder.Property(supplierProductVariant => supplierProductVariant.Code)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(supplierProductVariant => new { supplierProductVariant.Code, supplierProductVariant.SupplierProductID }).IsUnique();
        }
    }
}
