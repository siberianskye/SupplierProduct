using Microsoft.EntityFrameworkCore;
using SupplierProductExercise.Business.Entities;
using SupplierProductExercise.Business.Enums;
using SupplierProductExercise.Business.Repositories;
using SupplierProductExercise.Business.Services;
using SupplierProductExercise.Business.Services.Models;

namespace SupplierProductExercise.Business.Importers
{
    public class SupplierProductDataImporter : ISupplierProductDataImporter
    {
        private readonly ISupplierProductRepository __SupplierProductRepository;
        private readonly ISupplierProductService __SupplierProductService;

        public SupplierProductDataImporter(ISupplierProductRepository supplierProductRepository, ISupplierProductService supplierProductService)
        {
            __SupplierProductRepository = supplierProductRepository;
            __SupplierProductService = supplierProductService;
        }

        public async Task<SupplierProductImportResult> Import()
        {
            SupplierProductImportResult _Result = new();

            GetSupplierProductsServiceResult _ServiceResult = await __SupplierProductService.GetSupplierProducts(Service.ExampleSupplier);

            if (!_ServiceResult.IsSuccuessful)
            {
                return _Result;
            }

            List<SupplierProductModel> _ImportedSupplierProductModels = _ServiceResult.SupplierProductModels;

            List<SupplierProduct> _ImportedSupplierProducts = ConvertProductModelsToEntities(_ImportedSupplierProductModels);
            List<SupplierProduct> _ExistingProducts = await __SupplierProductRepository.GetSupplierProducts();


            _Result.Added = await SaveNewProducts(_ImportedSupplierProducts, _ExistingProducts);
            _Result.Updated = await UpdateExistingProducts(_ImportedSupplierProducts, _ExistingProducts);
            _Result.Discontinued = await DiscontinueProducts(_ImportedSupplierProducts, _ExistingProducts);
            _Result.IsSuccessful = true;

            return _Result;
        }

        private async Task<int> SaveNewProducts(List<SupplierProduct> importedSupplierProducts, List<SupplierProduct> existingProducts)
        {
            List<SupplierProduct> _NewProducts = importedSupplierProducts.Where(imported => !existingProducts.Any(existing => existing.SKU == imported.SKU)).ToList();


            return await __SupplierProductRepository.CreateProductsAsync(_NewProducts);
        }

        private async Task<int> UpdateExistingProducts(List<SupplierProduct> importedSupplierProducts, List<SupplierProduct> existingProducts)
        {
            List<SupplierProduct> _UpdatedProducts = importedSupplierProducts.Where(imported => existingProducts.Any(existing => existing.SKU == imported.SKU)).ToList();

            return (await __SupplierProductRepository.UpdateProductsAsync(_UpdatedProducts)).Count;
        }

        private async Task<int> DiscontinueProducts(List<SupplierProduct> importedSupplierProducts, List<SupplierProduct> existingProducts)
        {
            List<SupplierProduct> _DiscontinuedProducts = existingProducts.Where(existing => !existing.IsDiscontinued && !importedSupplierProducts.Any(imported => imported.SKU == existing.SKU)).ToList();

            return (await __SupplierProductRepository.DiscontinueProductsAsync(_DiscontinuedProducts)).Count;
        }

        private static List<SupplierProduct> ConvertProductModelsToEntities(List<SupplierProductModel> supplierProductModels)
            => supplierProductModels.ConvertAll(model => ConvertProductModelToEntity(model));

        private static SupplierProduct ConvertProductModelToEntity(SupplierProductModel supplierProductModel)
        {
            SupplierProduct _SupplierProduct = new()
            {
                SKU = supplierProductModel.SKU,
                Name = supplierProductModel.Name,
                Price = supplierProductModel.Price,
                UpdatedAtUtc = supplierProductModel.UpdatedAtUtc,
                Variants = ConvertProductVariantModelsToEntities(supplierProductModel.Variants)
            };

            return _SupplierProduct;
        }

        private static List<SupplierProductVariant> ConvertProductVariantModelsToEntities(List<SupplierProductVariantModel> supplierProductVariantModels)
            => supplierProductVariantModels.ConvertAll(model => ConvertProductVariantModelToEntity(model));

        private static SupplierProductVariant ConvertProductVariantModelToEntity(SupplierProductVariantModel supplierProductVariantModel)
        {
            SupplierProductVariant _SupplierProductVariant = new()
            {
                Code = supplierProductVariantModel.Code,
                StockQuantity = supplierProductVariantModel.StockQuantity
            };

            return _SupplierProductVariant;
        }
    }
}
