using SupplierProductExercise.Business.Enums;
using SupplierProductExercise.Business.Services.Models;

namespace SupplierProductExercise.Business.Services
{
    public interface ISupplierProductService
    {
        public Task<GetSupplierProductsServiceResult> GetSupplierProducts(Service service);
    }
}