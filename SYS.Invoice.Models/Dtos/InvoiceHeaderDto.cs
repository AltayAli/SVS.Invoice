using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace SVS.Invoice.Models.Dtos
{
    public class InvoiceHeaderDto
    {
        [JsonPropertyName("InvoiceId")]
        [JsonProperty("InvoiceId")]
        public string Id { get; set; }
        [JsonPropertyName("SenderTitle")]
        public string SenderTitle { get; set; }
        [JsonPropertyName("ReceiverTitle")]
        public string ReceiverTitle { get; set; }
        [JsonPropertyName("Date")]
        public string Date { get; set; }
    }
}