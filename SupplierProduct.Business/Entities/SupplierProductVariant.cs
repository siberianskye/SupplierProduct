namespace SupplierProductExercise.Business.Entities
{
    public class SupplierProductVariant
    {
        public int ID { get; set; }
        public int SupplierProductID { get; set;  }
        public required string Code { get; set; }
        public int StockQuantity { get; set; }
        public bool IsDiscontinued { get; set; }
    }
}
