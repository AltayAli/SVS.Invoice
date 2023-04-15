using SVS.Invoice.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SVS.Invoice.Models.Entities
{
    public class InvoiceRecord : InvoiceBase
    {
        public int Id { get; set; }
        [Required]
        public string Record { get; set; }
        public InvoiceRecordStatusEnum Status { get; set; } = InvoiceRecordStatusEnum.Waiting;
        public DateTime LastOperationDate { get; set; } = DateTime.Now;
        [StringLength(256)]
        public string FileName { get; set; }
    }
}
