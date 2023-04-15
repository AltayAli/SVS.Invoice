using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SVS.Invoice.Models.Entities
{
    public class InvoiceHeader : InvoiceBase
    {
        public InvoiceHeader()
        {
            InvoiceLines = new HashSet<InvoiceLine>();
        }
        [Key]
        [Required]
        public string Id { get; set; }
        [Required]
        [StringLength(256)]
        public string SenderTitle { get; set; }
        [Required]
        [StringLength(256)]
        public string ReceiverTitle { get; set; }
        [Required]
        public DateTime Date { get; set; }     
        
        public ICollection<InvoiceLine> InvoiceLines { get; set; }
    }
}