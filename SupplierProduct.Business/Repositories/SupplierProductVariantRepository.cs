using Microsoft.EntityFrameworkCore;
using SupplierProductExercise.Business.Context;
using SupplierProductExercise.Business.Entities;

namespace SupplierProductExercise.Business.Repositories
{
    public class SupplierProductVariantRepository : ISupplierProductVariantRepository
    {
        private readonly ISupplierProductDbContext __SupplierProductDbContext;

        public SupplierProductVariantRepository(ISupplierProductDbContext supplierProductDbContext)
        {
            __SupplierProductDbContext = supplierProductDbContext ?? throw new ArgumentNullException(nameof(supplierProductDbContext));
        }

        private async Task<int> CreateAsync(SupplierProductVariant supplierProductVariant)
        {
            if (supplierProductVariant == null || DoesProductVariantExist(supplierProductVariant.Code))
            {
                return 0;
            }

            await __SupplierProductDbContext.SupplierProductVariants.AddAsync(supplierProductVariant);


            return supplierProductVariant.ID;
        }

        public async Task<List<int>> CreateProductVariantsAsync(List<SupplierProductVariant> supplierProductVariants)
        {
            List<int> _CreatedIDs = new();

            if (supplierProductVariants.Count < 1)
            {
                return _CreatedIDs;
            }

            foreach (SupplierProductVariant _SupplierProductVariant in supplierProductVariants)
            {
                int _CreatedID = await CreateAsync(_SupplierProductVariant);

                if (_CreatedID > 0)
                {
                    _CreatedIDs.Add(_CreatedID);
                }
            }

            await __SupplierProductDbContext.SaveChangesAsync();

            return _CreatedIDs;
        }

        private bool DoesProductVariantExist(string code)
        {
            return __SupplierProductDbContext.SupplierProductVariants.FirstOrDefault(supplierProductVariant => supplierProductVariant.Code == code) != null;

        }

        private async Task<int> UpdateAsync(SupplierProductVariant supplierProductVariant)
        {
            if (supplierProductVariant == null || DoesProductVariantExist(supplierProductVariant.Code))
            {
                return 0;
            }

            SupplierProductVariant _SupplierProductVariant = await __SupplierProductDbContext.SupplierProductVariants.SingleAsync(product => product.ID == supplierProductVariant.ID);
            _SupplierProductVariant.SupplierProductID = supplierProductVariant.SupplierProductID;
            _SupplierProductVariant.Code = supplierProductVariant.Code;
            _SupplierProductVariant.StockQuantity = supplierProductVariant.StockQuantity;
            _SupplierProductVariant.IsDiscontinued = supplierProductVariant.IsDiscontinued;

            return supplierProductVariant.ID;
        }

        public async Task<List<int>> UpdateProductVariantsAsync(List<SupplierProductVariant> supplierProductVariants)
        {
            List<int> _UpdatedIDs = new();

            if (supplierProductVariants.Count < 1)
            {
                return _UpdatedIDs;
            }

            foreach (SupplierProductVariant _SupplierProductVariant in supplierProductVariants)
            {
                int _UpdatedID = await UpdateAsync(_SupplierProductVariant);

                if (_UpdatedID > 0)
                {
                    _UpdatedIDs.Add(_UpdatedID);
                }
            }

            await __SupplierProductDbContext.SaveChangesAsync();

            return _UpdatedIDs;
        }

    }
}