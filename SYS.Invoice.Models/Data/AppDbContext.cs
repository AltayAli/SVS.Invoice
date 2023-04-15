using Microsoft.EntityFrameworkCore;
using SVS.Invoice.Models.Entities;

namespace SVS.Invoice.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public virtual DbSet<InvoiceHeader> InvoiceHeaders { get; set; } 
        public virtual DbSet<InvoiceLine> InvoiceLines { get; set; } 
        public virtual DbSet<InvoiceRecord> InvoiceRecords { get; set; } 
    }
}