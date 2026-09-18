namespace SupplierProductExercise.Business.Entities
{
    public class SupplierProduct
    {
        public int ID { get; set; }
        public required string SKU { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime UpdatedAtUtc { get; set; }
        public bool IsDiscontinued { get; set; }
        public List<SupplierProductVariant> Variants { get; set; }
    }
}
