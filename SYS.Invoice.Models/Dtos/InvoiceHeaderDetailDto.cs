using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SVS.Invoice.Models.Dtos
{
    public class InvoiceHeaderDetailDto
    {
        [JsonPropertyName("InvoiceId")]
        public string Id { get; set; }
        [JsonPropertyName("SenderTitle")]
        public string SenderTitle { get; set; }
        [JsonPropertyName("ReceiverTitle")]
        public string ReceiverTitle { get; set; }
        [JsonPropertyName("Date")]
        public string Date { get; set; }
        [JsonPropertyName("InvoiceLine")]
        public List<InvoiceLineDto> InvoiceLine { get; set; }
    }
}