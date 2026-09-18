namespace SupplierProductExercise.Business.Services.Models
{
    public class GetSupplierProductsServiceResult
    {
        public bool IsSuccuessful { get; set; }
        public List<SupplierProductModel> SupplierProductModels { get; set; }
    }
}
