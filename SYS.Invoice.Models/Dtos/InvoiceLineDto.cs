using System.Text.Json.Serialization;

namespace SVS.Invoice.Models.Dtos
{
    public class InvoiceLineDto
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("UnitCode")]
        public string UnitCode { get; set; }
        [JsonPropertyName("UnitPrice")]
        public float UnitPrice { get; set; }
    }
}