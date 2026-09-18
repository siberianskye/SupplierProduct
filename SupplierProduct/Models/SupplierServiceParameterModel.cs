using SupplierProductExercise.Business.Enums;

namespace SupplierProductExercise.Models
{
    public class SupplierServiceParameterModel
    {
        public Service Service { get; set; }
        public string? BearerToken { get; set; }
    }
}
