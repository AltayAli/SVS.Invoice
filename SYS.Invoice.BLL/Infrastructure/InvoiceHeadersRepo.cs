using Microsoft.EntityFrameworkCore;
using SVS.Invoice.Models.Data;
using SVS.Invoice.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SYS.Invoice.BLL.Infrastructure
{
    public class InvoiceHeadersRepo : IInvoiceHeadersRepo
    {
        private readonly AppDbContext _dbContext;

        public InvoiceHeadersRepo(AppDbContext dbContext)
        {
           _dbContext = dbContext;
        }
        public async Task<List<InvoiceHeader>> GetList(int offset, int limit=10)
            => await  _dbContext.InvoiceHeaders.Skip(offset).Take(limit).ToListAsync();

        public Task<InvoiceHeader> Get(string invoiceId)
        {
            InvoiceHeader invoiceHeader = _dbContext.InvoiceHeaders.Include(x=>x.InvoiceLines).FirstOrDefault(x => x.Id == invoiceId);
            if (invoiceHeader == null)
            {
                throw new NullReferenceException($"Tried Id : {invoiceId} --- Header not found.");
            }

            return Task.Run(()=> invoiceHeader);
        }

        public async Task Insert(InvoiceHeader invoiceHeader)
        {
            InvoiceHeader existedInvoiceHeader = _dbContext.InvoiceHeaders.FirstOrDefault(x => x.Id == invoiceHeader.Id);
            if (existedInvoiceHeader != null)
                Update(existedInvoiceHeader, invoiceHeader);
            else
               await _dbContext.InvoiceHeaders.AddAsync(invoiceHeader);
        }

        public void Update(InvoiceHeader existedInvoiceHeader, InvoiceHeader invoiceHeader)
        {
            existedInvoiceHeader.SenderTitle = invoiceHeader.SenderTitle;
            existedInvoiceHeader.ReceiverTitle = invoiceHeader.ReceiverTitle;
            existedInvoiceHeader.Date = invoiceHeader.Date;
            existedInvoiceHeader.LastUpdatedDate = DateTime.Now;
        }

    }
}