using Microsoft.EntityFrameworkCore;
using SupplierProductExercise.Business.Context;
using SupplierProductExercise.Business.Entities;
using SupplierProductExercise.Business.Enums;

namespace SupplierProductExercise.Business.Repositories
{
    public class SupplierServiceParameterRepository : ISupplierServiceParameterRepository
    {
        private readonly ISupplierProductDbContext __SupplierProductDbContext;

        public SupplierServiceParameterRepository(ISupplierProductDbContext supplierProductDbContext)
        {
            __SupplierProductDbContext = supplierProductDbContext ?? throw new ArgumentNullException(nameof(supplierProductDbContext));
        }

        public async Task<SupplierServiceParameter> GetByService(Service service)
        {
            return await __SupplierProductDbContext.SupplierServiceParameters.SingleOrDefaultAsync(parameter => parameter.Service == service);
        }

        public async Task<int> CreateAsync(SupplierServiceParameter supplierServiceParameter)
        {
            if (supplierServiceParameter == null || DoesServiceParameterExist(supplierServiceParameter.Service))
            {
                return 0;
            }

            await __SupplierProductDbContext.SupplierServiceParameters.AddAsync(supplierServiceParameter);

            await __SupplierProductDbContext.SaveChangesAsync();

            return supplierServiceParameter.ID;
        }

        private bool DoesServiceParameterExist(Service service)
        {
            return __SupplierProductDbContext.SupplierServiceParameters.FirstOrDefault(parameter => parameter.Service == service) != null;
        }

        public async Task<int> UpdateAsync(SupplierServiceParameter supplierServiceParameter)
        {
            if (supplierServiceParameter == null || !DoesServiceParameterExist(supplierServiceParameter.Service))
            {
                return 0;
            }

            SupplierServiceParameter _SupplierServiceParameter = await __SupplierProductDbContext.SupplierServiceParameters.SingleAsync(parameter => parameter.ID == supplierServiceParameter.ID);

            _SupplierServiceParameter.BearerToken = supplierServiceParameter.BearerToken;

            await __SupplierProductDbContext.SaveChangesAsync();

            return supplierServiceParameter.ID;
        }
    }
}
