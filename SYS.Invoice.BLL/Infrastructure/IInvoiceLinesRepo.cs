using SVS.Invoice.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SYS.Invoice.BLL.Infrastructure
{
    public interface IInvoiceLinesRepo
    {
        Task Insert(string invoiceId, List<InvoiceLine> invoiceLines);
    }
}