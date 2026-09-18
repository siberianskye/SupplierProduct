namespace SupplierProductExercise.Business.Importers
{
    public interface ISupplierProductDataImporter
    {
        public Task<SupplierProductImportResult> Import();
    }
}
