namespace SupplierProductExercise.Business.Importers
{
    public class SupplierProductImportResult
    {
        public int Added { get; set; }
        public int Updated { get; set; }
        public int Discontinued { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
