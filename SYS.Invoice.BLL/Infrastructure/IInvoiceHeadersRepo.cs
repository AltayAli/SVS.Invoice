using SVS.Invoice.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SYS.Invoice.BLL.Infrastructure
{
    public interface IInvoiceHeadersRepo
    {
        Task<List<InvoiceHeader>> GetList(int offset, int limit);
        Task<InvoiceHeader> Get(string invoiceId);
        Task Insert(InvoiceHeader invoiceHeader);
    }
}