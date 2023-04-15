using SYS.Invoice.Models.Entities;
using System;
using System.Collections.Generic;

namespace SVS.Invoice.UnitTests.Models
{
    public static class FakeDataHelper
    {
        public static List<InvoiceHeader> GenerateInvoiceHeadersList()
            => new List<InvoiceHeader>
            {
                new InvoiceHeader { Id = "1", SenderTitle = "Sender1", ReceiverTitle = "Receiver1", Date = DateTime.Now },
                new InvoiceHeader { Id = "2", SenderTitle = "Sender2", ReceiverTitle = "Receiver2", Date = DateTime.Now },
                new InvoiceHeader { Id = "3", SenderTitle = "Sender3", ReceiverTitle = "Receiver3", Date = DateTime.Now },
            };
        public static List<InvoiceLine> GenerateInvoiceLinesList()
            => new List<InvoiceLine>
            {
                new InvoiceLine { Id = 1, Name = "1.Ürün", Quantity = 5,  UnitCode = InvoiceLineUnitCodeEnum.Adet, UnitPrice = 10,HeaderId = "1" },
                new InvoiceLine { Id = 2, Name = "2.Ürün", Quantity = 2, UnitCode = InvoiceLineUnitCodeEnum.Litre, UnitPrice = 3 ,HeaderId = "1"},
                new InvoiceLine { Id = 3,  Name = "3.Ürün", Quantity = 25, UnitCode = InvoiceLineUnitCodeEnum.Kilogram, UnitPrice = 2 ,HeaderId = "1"}
            };

        public static List<InvoiceRecord> GenerateInvoiceRecordsList()
            => new List<InvoiceRecord>
        {
            new InvoiceRecord { Id = 1, Record = "Record1", LastOperationDate = DateTime.Now, FileName = "File1.txt" },
            new InvoiceRecord { Id = 2, Record = "Record2", LastOperationDate = DateTime.Now, FileName = "File2.txt" },
            new InvoiceRecord { Id = 3, Record = "Record3", LastOperationDate = DateTime.Now, FileName = "File3.txt" }
        };
    }
}
