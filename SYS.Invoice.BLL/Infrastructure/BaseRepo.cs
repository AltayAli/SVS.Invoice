using SVS.Invoice.Models.Data;
using System.Threading.Tasks;

namespace SYS.Invoice.BLL.Infrastructure
{
    public class BaseRepo : IBaseRepo
    {
        private IInvoiceRecordsRepo _invoiceRecordsRepo;
        private IInvoiceLinesRepo _invoiceLinesRepo;
        private IInvoiceHeadersRepo _invoiceHeadersRepo;
        private readonly AppDbContext _appDbContext;

        public BaseRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IInvoiceRecordsRepo InvoiceRecordsRepo => _invoiceRecordsRepo ??= new InvoiceRecordsRepo(_appDbContext);
        public IInvoiceLinesRepo InvoiceLinesRepo => _invoiceLinesRepo ??= new InvoiceLinesRepo(_appDbContext);
        public IInvoiceHeadersRepo InvoiceHeadersRepo => _invoiceHeadersRepo ??= new InvoiceHeadersRepo(_appDbContext);

        public Task SaveChanges()
            => _appDbContext.SaveChangesAsync();
    }
}