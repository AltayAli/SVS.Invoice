using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using SVS.Invoice.Models.Data;
using SVS.Invoice.Models.Entities;
using SVS.Invoice.UnitTests.Models;
using SYS.Invoice.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SVS.Invoice.UnitTests.Tests
{

    [TestFixture]
    public class InvoiceHeadersTests
    {
        private InvoiceHeadersRepo _invoiceHeaderRepo;
        private Mock<AppDbContext> _dbContextMock;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<AppDbContext> options = new DbContextOptions<AppDbContext>();
            _dbContextMock = new Mock<AppDbContext>(options);
            _invoiceHeaderRepo = new InvoiceHeadersRepo(_dbContextMock.Object);
        }

        [Test]
        public async Task InvoiceHeader_GetList_Test()
        {
            List<InvoiceHeader> invoiceHeader = FakeDataHelper.GenerateInvoiceHeadersList().ToList();
            var mock = invoiceHeader.BuildMock().BuildMockDbSet();

            int offset = 0, limit = 2;

            _dbContextMock.Setup(db => db.InvoiceHeaders).Returns(mock.Object);

            List<InvoiceHeader> result = (await _invoiceHeaderRepo.GetList(offset, limit));

            Assert.IsTrue(result != null);
            Assert.AreEqual(limit, result.Count);
            Assert.AreEqual(invoiceHeader[0].SenderTitle, result[0].SenderTitle);
            Assert.AreEqual(invoiceHeader[0].ReceiverTitle, result[0].ReceiverTitle);
            Assert.AreEqual(invoiceHeader[1].SenderTitle, result[1].SenderTitle);
            Assert.AreEqual(invoiceHeader[1].ReceiverTitle, result[1].ReceiverTitle);
        }

        [Test]
        public async Task InvoiceHeader_Get_By_Id_Success_Test()
        {
            List<InvoiceHeader> invoiceHeader = FakeDataHelper.GenerateInvoiceHeadersList().OrderBy(x => x.Date).ToList();
            var mock = invoiceHeader.BuildMock().BuildMockDbSet();

            _dbContextMock.Setup(db => db.InvoiceHeaders).Returns(mock.Object);

            InvoiceHeader result = (await _invoiceHeaderRepo.Get("1"));

            Assert.IsTrue(result != null);
            Assert.AreEqual(invoiceHeader[0].SenderTitle, result.SenderTitle);
            Assert.AreEqual(invoiceHeader[0].ReceiverTitle, result.ReceiverTitle);
        }

        [Test]
        public  void InvoiceHeader_Get_By_Id_Not_Found_Test()
        {
            List<InvoiceHeader> invoiceHeader = FakeDataHelper.GenerateInvoiceHeadersList().ToList();
            var mock = invoiceHeader.BuildMock().BuildMockDbSet();

            _dbContextMock.Setup(db => db.InvoiceHeaders).Returns(mock.Object);

            Assert.Throws<NullReferenceException>(()=>  _invoiceHeaderRepo.Get("22").GetAwaiter().GetResult());
        }

        [Test]
        public async Task InvoiceHeader_Insertion_Test()
        {
            InvoiceHeader invoiceHeader = new InvoiceHeader { Id = "4", SenderTitle = "Sender4", ReceiverTitle = "Receiver4", Date = DateTime.Now };

            var mock = FakeDataHelper.GenerateInvoiceHeadersList().BuildMock().BuildMockDbSet();
            _dbContextMock.Setup(db => db.InvoiceHeaders).Returns(mock.Object);

            _dbContextMock.Setup(db => db.InvoiceHeaders.AddAsync(invoiceHeader, new CancellationToken()));

            await _invoiceHeaderRepo.Insert(invoiceHeader);

            _dbContextMock.Verify(db => db.InvoiceHeaders.AddAsync(invoiceHeader, new CancellationToken()), Times.Exactly(1));
        }

        [Test]
        public async Task InvoiceHeader_Update_Test()
        {
            InvoiceHeader invoiceHeader = new InvoiceHeader { Id = "2", SenderTitle = "Sender22", ReceiverTitle = "Receiver22", Date = DateTime.Now };

            var mock = FakeDataHelper.GenerateInvoiceHeadersList().BuildMock().BuildMockDbSet();
            _dbContextMock.Setup(db => db.InvoiceHeaders).Returns(mock.Object);

            _dbContextMock.Setup(db => db.InvoiceHeaders.AddAsync(invoiceHeader, new CancellationToken()));

            await _invoiceHeaderRepo.Insert(invoiceHeader);

            _dbContextMock.Verify(db => db.InvoiceHeaders.AddAsync(invoiceHeader, new CancellationToken()), Times.Exactly(0));
        }
    }
}
