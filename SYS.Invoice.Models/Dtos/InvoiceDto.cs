using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SVS.Invoice.Models.Dtos
{
    public class InvoiceDto
    {
        [JsonPropertyName("InvoiceHeader")]
        public InvoiceHeaderDto InvoiceHeader { get; set; }
        [JsonPropertyName("InvoiceLine")]
        public List<InvoiceLineDto> InvoiceLine { get; set; }
    }
}