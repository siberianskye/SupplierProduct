using System.Text.Json.Serialization;

namespace SupplierProductExercise.Business.Services.Models
{
    [Serializable]
    public class SupplierProductModel
    {
        [JsonPropertyName("sku")]
        public required string SKU { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("updatedAtUtc")]
        public DateTime UpdatedAtUtc { get; set; }

        [JsonPropertyName("variants")]
        public List<SupplierProductVariantModel> Variants { get; set; }
    }
}
