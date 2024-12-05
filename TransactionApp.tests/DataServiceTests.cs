using Microsoft.EntityFrameworkCore;
using Moq;
using TransactionApp.Data;
using TransactionApp.Models;
using TransactionApp.Services;
using Xunit;

namespace TransactionApp.Tests.Services
{
    public class DataServiceTests
    {
        private readonly TransactionContext _context;
        private readonly Mock<IExternalDataService> _mockExternalService;
        private readonly DataService _service;

        public DataServiceTests()
        {
            var options = new DbContextOptionsBuilder<TransactionContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new TransactionContext(options);
            _mockExternalService = new Mock<IExternalDataService>();
            _service = new DataService(_context, _mockExternalService.Object);
        }

        [Fact]
        public async Task ProcessTransactionsAsync_StoresNewTransactions()
        {

            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    TransactionId = "test1",
                    ProductName = "Product 1",
                    QuantitySold = 10,
                    PricePerUnit = 10m,
                    TotalSaleAmount = 100m,
                    TransactionDate = DateTime.UtcNow,
                    Region = "North"
                }
            };

            _mockExternalService
                .Setup(x => x.FetchTransactionsAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(transactions);

            await _service.ProcessTransactionsAsync(DateTime.UtcNow, DateTime.UtcNow.AddDays(1));

            Assert.Equal(1, await _context.Transactions.CountAsync());
        }
    }
}