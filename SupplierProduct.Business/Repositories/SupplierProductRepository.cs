using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SupplierProductExercise.Business.Context;
using SupplierProductExercise.Business.Entities;
using SupplierProductExercise.Business.Extensions;


namespace SupplierProductExercise.Business.Repositories
{
    public class SupplierProductRepository : ISupplierProductRepository
    {
        private readonly ISupplierProductDbContext __SupplierProductDbContext;

        public SupplierProductRepository(ISupplierProductDbContext supplierProductDbContext)
        {
            __SupplierProductDbContext = supplierProductDbContext ?? throw new ArgumentNullException(nameof(supplierProductDbContext));
        }

        public async Task<List<SupplierProduct>> GetSupplierProducts()
        {
            return await GetSupplierProducts(true, 0);
        }

        public async Task<List<SupplierProduct>> GetSupplierProducts(bool includeDiscontinued, int minimumStock)
        {
            List<SupplierProduct> _SupplierProducts = [];

            if (!__SupplierProductDbContext.SupplierProducts.IsNullOrEmpty())
            {
                _SupplierProducts = __SupplierProductDbContext.SupplierProducts.Include(supplierProduct => supplierProduct.Variants.OrderBy(variant => variant.Code))
                                           .WhereIf(!includeDiscontinued, supplierProduct => !supplierProduct.IsDiscontinued)
                                           .WhereIf(minimumStock > 0, supplierProduct => supplierProduct.Variants.Any(variant => variant.StockQuantity >= minimumStock))                                        
                                           .OrderBy(supplierProduct => supplierProduct.SKU).ToList();          
            }         

            return _SupplierProducts;
        }

        public async Task<SupplierProduct> GetBySKUAsync(string sku)
        {
            return await __SupplierProductDbContext.SupplierProducts.Include(supplierProduct => supplierProduct.Variants).SingleOrDefaultAsync(supplierProduct => supplierProduct.SKU == sku);
        }

        private async Task<int> CreateAsync(SupplierProduct supplierProduct)
        {
            if (supplierProduct == null || DoesProductExist(supplierProduct.SKU))
            {
                return 0;
            }

            await __SupplierProductDbContext.SupplierProducts.AddAsync(supplierProduct);

            return 1;
        }

        public async Task<int> CreateProductsAsync(List<SupplierProduct> supplierProducts)
        {
            int _CreatedCount = 0;

            if (supplierProducts.Count < 1)
            {
                return _CreatedCount;
            }

            foreach (SupplierProduct _SupplierProduct in supplierProducts)
            {
                int _CreatedID = await CreateAsync(_SupplierProduct);

                if (_CreatedID > 0)
                {
                    _CreatedCount++;
                }
            }

            await __SupplierProductDbContext.SaveChangesAsync();

            return _CreatedCount;
        }

        private bool DoesProductExist(string sku)
        {
            return __SupplierProductDbContext.SupplierProducts.FirstOrDefault(supplierProduct => supplierProduct.SKU == sku) != null;
        }

        private async Task<int> UpdateAsync(SupplierProduct supplierProduct)
        {
            if (supplierProduct == null || !DoesProductExist(supplierProduct.SKU))
            {
                return 0;
            }

            SupplierProduct _SupplierProduct = await __SupplierProductDbContext.SupplierProducts.SingleAsync(product => product.SKU == supplierProduct.SKU);

            _SupplierProduct.Name = supplierProduct.Name;
            _SupplierProduct.Price = supplierProduct.Price;
            _SupplierProduct.UpdatedAtUtc = supplierProduct.UpdatedAtUtc;
            _SupplierProduct.IsDiscontinued = false;
            _SupplierProduct.Variants.AddRange(supplierProduct.Variants.Where(imported => !_SupplierProduct.Variants.Any(existing => existing.Code == imported.Code)));

            return _SupplierProduct.ID;
        }

        public async Task<List<int>> UpdateProductsAsync(List<SupplierProduct> supplierProducts)
        {
            List<int> _UpdatedIDs = new();

            if (supplierProducts.Count < 1)
            {
                return _UpdatedIDs;
            }

            foreach (SupplierProduct _SupplierProduct in supplierProducts)
            {
                int _Updated_ID = await UpdateAsync(_SupplierProduct);

                if (_Updated_ID > 0)
                {
                    _UpdatedIDs.Add(_Updated_ID);
                }
            }

            await __SupplierProductDbContext.SaveChangesAsync();

            return _UpdatedIDs;
        }

        private async Task<int> DiscontinueProductAsync(SupplierProduct supplierProduct)
        {
            if (supplierProduct == null || !DoesProductExist(supplierProduct.SKU))
            {
                return 0;
            }

            SupplierProduct _SupplierProduct = await __SupplierProductDbContext.SupplierProducts.SingleAsync(product => product.SKU == supplierProduct.SKU);

            _SupplierProduct.IsDiscontinued = true;
            foreach (SupplierProductVariant _Variant in _SupplierProduct.Variants)
            {
                _Variant.IsDiscontinued = true;
            }

            return supplierProduct.ID;
        }

        public async Task<List<int>> DiscontinueProductsAsync(List<SupplierProduct> supplierProducts)
        {
            List<int> _UpdatedIDs = new();

            if (supplierProducts.Count < 1)
            {
                return _UpdatedIDs;
            }

            foreach (SupplierProduct _SupplierProduct in supplierProducts)
            {
                int _Updated_ID = await DiscontinueProductAsync(_SupplierProduct);

                if (_Updated_ID > 0)
                {
                    _UpdatedIDs.Add(_Updated_ID);
                }
            }

            await __SupplierProductDbContext.SaveChangesAsync();

            return _UpdatedIDs;
        }
    }
}