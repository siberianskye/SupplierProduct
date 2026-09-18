using SupplierProductExercise.Business.Entities;

namespace SupplierProductExercise.Business.Repositories
{
    public interface ISupplierProductRepository
    {
        public Task<List<SupplierProduct>> GetSupplierProducts();
        public Task<List<SupplierProduct>> GetSupplierProducts(bool includeDiscontinued, int minimumStock);
        public Task<int> CreateProductsAsync(List<SupplierProduct> supplierProducts);
        public Task<SupplierProduct> GetBySKUAsync(string sku);
        public Task<List<int>> DiscontinueProductsAsync(List<SupplierProduct> supplierProducts);
        public Task<List<int>> UpdateProductsAsync(List<SupplierProduct> supplierProducts);
    }
}