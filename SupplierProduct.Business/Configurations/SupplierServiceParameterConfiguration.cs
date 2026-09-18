using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplierProductExercise.Business.Entities;

namespace SupplierProductExercise.Business.Configurations
{
    public class SupplierServiceParameterConfiguration : IEntityTypeConfiguration<SupplierServiceParameter>
    {
        public void Configure(EntityTypeBuilder<SupplierServiceParameter> builder)
        {
            builder.ToTable("SupplierServiceParameter", "SupplierProductExercise");

            builder.Property(supplierServiceParameter => supplierServiceParameter.BearerToken)
                .HasMaxLength(100);

            builder.HasIndex(supplierServiceParameter => new { supplierServiceParameter.Service }).IsUnique();

        }
    }
}
