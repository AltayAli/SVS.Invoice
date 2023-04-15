using System.Threading.Tasks;

namespace SYS.Invoice.BLL.Infrastructure
{
    public interface IBaseRepo
    {
        IInvoiceRecordsRepo InvoiceRecordsRepo { get; }
        IInvoiceLinesRepo InvoiceLinesRepo { get; }
        IInvoiceHeadersRepo InvoiceHeadersRepo { get; }
        Task SaveChanges();
    }
}