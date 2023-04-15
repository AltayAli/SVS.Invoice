using Microsoft.EntityFrameworkCore;
using SVS.Invoice.Models.Data;
using SVS.Invoice.Models.Entities;
using SVS.Invoice.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SYS.Invoice.BLL.Infrastructure
{
    public class InvoiceRecordsRepo : IInvoiceRecordsRepo
    {
        private readonly AppDbContext _dbContext;

        public InvoiceRecordsRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<InvoiceRecord>> GetList(InvoiceRecordStatusEnum status)
            => await _dbContext.InvoiceRecords.AsNoTracking().Where(x => x.Status == status).ToListAsync();

        public async Task Insert(InvoiceRecord record)
        {
           await _dbContext.InvoiceRecords.AddAsync(record);
        }

        public Task Update(InvoiceRecord record)
        {
            _dbContext.InvoiceRecords.Update(record);
            
            return Task.CompletedTask;
        }
    }
}