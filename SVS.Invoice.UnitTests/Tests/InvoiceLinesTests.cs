using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using SVS.Invoice.UnitTests.Models;
using SYS.Invoice.BLL.Infrastructure;
using SVS.Invoice.Models.Data;
using SVS.Invoice.Models.Entities;
using SVS.Invoice.Models.Enums;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SVS.Invoice.UnitTests.Tests
{
    [TestFixture]
    public class InvoiceLinesTests
    {
        private InvoiceLinesRepo _invoiceLinesRepo;
        private Mock<AppDbContext> _dbContextMock;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<AppDbContext> options = new DbContextOptions<AppDbContext>();
            _dbContextMock = new Mock<AppDbContext>(options);
            _invoiceLinesRepo = new InvoiceLinesRepo(_dbContextMock.Object);
        }

        [Test]
        public async Task InvoiceLine_Insertion_Test()
        {
            string _headerId = "1";
            InvoiceLine invoiceLine = new InvoiceLine { Id = 4, Name = "4.Ürün", Quantity = 5, UnitCode = InvoiceLineUnitCodeEnum.Adet, UnitPrice = 10};

            var mock = FakeDataHelper.GenerateInvoiceLinesList().BuildMock().BuildMockDbSet();
            _dbContextMock.Setup(db => db.InvoiceLines).Returns(mock.Object);

            _dbContextMock.Setup(db => db.InvoiceLines.AddAsync(invoiceLine, new CancellationToken()));

            await _invoiceLinesRepo.Insert(_headerId, new List<InvoiceLine> { invoiceLine });

            _dbContextMock.Verify(db => db.InvoiceLines.AddAsync(invoiceLine, new CancellationToken()), Times.Exactly(1));
        }

        [Test]
        public async Task InvoiceLine_Update_Test()
        {
            string _headerId = "1";
            InvoiceLine invoiceLine = new InvoiceLine { Id = 1, Name = "1.Ürün", Quantity = 5, UnitCode = InvoiceLineUnitCodeEnum.Adet, UnitPrice = 10 };

            var mock = FakeDataHelper.GenerateInvoiceLinesList().BuildMock().BuildMockDbSet();
            _dbContextMock.Setup(db => db.InvoiceLines).Returns(mock.Object);

            _dbContextMock.Setup(db => db.InvoiceLines.AddAsync(invoiceLine, new CancellationToken()));

            await _invoiceLinesRepo.Insert(_headerId, new List<InvoiceLine> { invoiceLine });

            _dbContextMock.Verify(db => db.InvoiceLines.AddAsync(invoiceLine, new CancellationToken()), Times.Exactly(0));
        }
    }
}
