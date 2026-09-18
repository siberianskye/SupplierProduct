using SupplierProductExercise.Business.Entities;

namespace SupplierProductExercise.Business.Repositories
{
    public interface ISupplierProductVariantRepository
    {
        public Task<List<int>> CreateProductVariantsAsync(List<SupplierProductVariant> supplierProductVariants);
        public Task<List<int>> UpdateProductVariantsAsync(List<SupplierProductVariant> supplierProductVariants);
    }
}