using System.Collections.Generic;
using System.Threading.Tasks;
using SVS.Invoice.Models.Entities;
using SVS.Invoice.Models.Enums;

namespace SYS.Invoice.BLL.Infrastructure
{
    public interface IInvoiceRecordsRepo
    {
        Task<List<InvoiceRecord>> GetList(InvoiceRecordStatusEnum status);
        Task Insert(InvoiceRecord record);
        Task Update(InvoiceRecord record);
    }
}