using SupplierProductExercise.Business.Enums;

namespace SupplierProductExercise.Business.Entities
{
    public class SupplierServiceParameter
    {
        public int ID { get; set;  }
        public Service Service { get; set; }
        public string? BearerToken { get; set;  }
    }
}
