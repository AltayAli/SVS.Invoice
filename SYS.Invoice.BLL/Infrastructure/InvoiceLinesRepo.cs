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
    public class InvoiceLinesRepo : IInvoiceLinesRepo
    {
        private readonly AppDbContext _dbContext;

        public InvoiceLinesRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Insert(string invoiceId, List<InvoiceLine> invoiceLines)
        {
            foreach (InvoiceLine invoiceLine in invoiceLines)
            {
                if (IsExist(invoiceLine.Id,out InvoiceLine existedInvoiceLine))
                {
                    Update(existedInvoiceLine, invoiceLine);
                }
                else
                {
                    invoiceLine.HeaderId = invoiceId;
                    await _dbContext.InvoiceLines.AddAsync(invoiceLine);
                }
            }
        }

        private void Update(InvoiceLine existedInvoiceLine, InvoiceLine invoiceLine)
        {
            existedInvoiceLine.Name = invoiceLine.Name;
            existedInvoiceLine.Quantity = invoiceLine.Quantity;
            existedInvoiceLine.UnitCode = invoiceLine.UnitCode;
            existedInvoiceLine.UnitPrice = invoiceLine.UnitPrice;
            existedInvoiceLine.LastUpdatedDate = DateTime.Now;
        }

        private bool IsExist(int invoiceId, out InvoiceLine existedInvoiceLine)
        {
            existedInvoiceLine = _dbContext.InvoiceLines.FirstOrDefault(x => x.Id == invoiceId);

            return existedInvoiceLine != null;
        }

    }
}