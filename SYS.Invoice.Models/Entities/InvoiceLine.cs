using SVS.Invoice.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SVS.Invoice.Models.Entities
{
    public class InvoiceLine : InvoiceBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public InvoiceLineUnitCodeEnum UnitCode { get; set; }
        [Required]
        public float UnitPrice { get; set; }

        [ForeignKey(nameof(Header))]
        public string HeaderId { get; set; }
        public InvoiceHeader Header { get; set; }
    }
}
