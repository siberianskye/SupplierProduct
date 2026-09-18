using System.Text.Json.Serialization;

namespace SupplierProductExercise.Business.Services.Models
{
    [Serializable]
    public class SupplierProductVariantModel
    {
        [JsonPropertyName("code")]
        public required string Code { get; set; }

        [JsonPropertyName("stockQuantity")]
        public int StockQuantity { get; set; }
    }
}
