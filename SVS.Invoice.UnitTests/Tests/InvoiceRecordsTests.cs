using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using SVS.Invoice.UnitTests.Models;
using SYS.Invoice.BLL.Infrastructure;
using SVS.Invoice.Models.Data;
using SVS.Invoice.Models.Entities;
using SVS.Invoice.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SVS.Invoice.UnitTests.Tests
{
    public class InvoiceRecordsTests
    {
        private InvoiceRecordsRepo _invoiceRecordsRepo;
        private Mock<AppDbContext> _dbContextMock;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<AppDbContext> options = new DbContextOptions<AppDbContext>();
            _dbContextMock = new Mock<AppDbContext>(options);
            _invoiceRecordsRepo = new InvoiceRecordsRepo(_dbContextMock.Object);
        }
        [Test]
        public async Task InvoiceRecord_GetList_Test()
        {
            List<InvoiceRecord> invoiceRecords = FakeDataHelper.GenerateInvoiceRecordsList().ToList();
            var mock = invoiceRecords.BuildMock().BuildMockDbSet();

            _dbContextMock.Setup(db => db.InvoiceRecords).Returns(mock.Object);

            List<InvoiceRecord> result = (await _invoiceRecordsRepo.GetList(InvoiceRecordStatusEnum.Waiting));

            Assert.IsTrue(result != null);
            Assert.AreEqual(invoiceRecords[0].Record, result[0].Record);
            Assert.AreEqual(invoiceRecords[0].FileName, result[0].FileName);
        }

        [Test]
        public async Task InvoiceRecord_Insertion_Test()
        {
            InvoiceRecord invoiceRecord = new InvoiceRecord { Record = "Record1", LastOperationDate = DateTime.Now, FileName = "File1.txt" };

            var mock = FakeDataHelper.GenerateInvoiceRecordsList().BuildMock().BuildMockDbSet();
            _dbContextMock.Setup(db => db.InvoiceRecords).Returns(mock.Object);

            _dbContextMock.Setup(db => db.InvoiceRecords.AddAsync(invoiceRecord, new CancellationToken()));

            await _invoiceRecordsRepo.Insert(invoiceRecord);

            _dbContextMock.Verify(db => db.InvoiceRecords.AddAsync(invoiceRecord, new CancellationToken()), Times.Exactly(1));
        }

        [Test]
        public async Task InvoiceRecord_Update_Test()
        {
            InvoiceRecord invoiceRecord = new InvoiceRecord { Record = "Record1", LastOperationDate = DateTime.Now, FileName = "File1.txt" };

            var mock = FakeDataHelper.GenerateInvoiceRecordsList().BuildMock().BuildMockDbSet();
            _dbContextMock.Setup(db => db.InvoiceRecords).Returns(mock.Object);

            _dbContextMock.Setup(db => db.InvoiceRecords.Update(invoiceRecord));

            await _invoiceRecordsRepo.Update(invoiceRecord);

            _dbContextMock.Verify(db => db.InvoiceRecords.Update(invoiceRecord), Times.Exactly(1));
        }
    }
}
