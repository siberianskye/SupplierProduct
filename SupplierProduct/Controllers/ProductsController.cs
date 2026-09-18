using Microsoft.AspNetCore.Mvc;
using SupplierProductExercise.Business.Entities;
using SupplierProductExercise.Business.Importers;
using SupplierProductExercise.Business.Repositories;

namespace SupplierProductExercise.Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ISupplierProductDataImporter __SupplierProductDataImporter;
        private readonly ISupplierProductRepository __SupplierProductRepository;
        public ProductsController(ISupplierProductDataImporter supplierProductDataImporter, ISupplierProductRepository supplierProductRepository)
        {
            __SupplierProductDataImporter = supplierProductDataImporter;
            __SupplierProductRepository = supplierProductRepository;
        }

        [HttpGet]
        [Route("api/products")]
        public async Task<IActionResult> Get(bool includeDiscontinued, int minimumStock)
        {
            if(minimumStock < 0)
            {
                return BadRequest("Minimum stock cannot be less than zero.");
            }

            return Ok(await __SupplierProductRepository.GetSupplierProducts(includeDiscontinued, minimumStock));
        }

        [HttpGet]
        [Route("api/products/{sku}")]
        public async Task<IActionResult> Get(string sku)
        {
            SupplierProduct _SupplierProduct = await __SupplierProductRepository.GetBySKUAsync(sku);

            if(_SupplierProduct == null)
            {
                return NotFound($"No product found with the SKU: {sku}");
            }

            return Ok(_SupplierProduct);
        }

        [HttpPost]
        [Route("api/products/import")]
        public async Task<IActionResult> Import()
        {
            SupplierProductImportResult _Result = new();
            _Result = await __SupplierProductDataImporter.Import();

            return Ok(_Result);
        }
    }
}
