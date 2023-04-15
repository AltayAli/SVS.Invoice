using System;

namespace SVS.Invoice.Models.Entities
{
    public class InvoiceBase
    {
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;
    }
}