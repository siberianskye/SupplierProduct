using SupplierProductExercise.Business.Entities;
using SupplierProductExercise.Business.Enums;

namespace SupplierProductExercise.Business.Repositories
{
    public interface ISupplierServiceParameterRepository
    {
        public Task<SupplierServiceParameter> GetByService(Service service);
        public Task<int> CreateAsync(SupplierServiceParameter supplierServiceParameter);
        public Task<int> UpdateAsync(SupplierServiceParameter supplierServiceParameter);
    }
}