using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TransactionApp.Controllers;
using TransactionApp.Data;
using TransactionApp.Models;
using Xunit;

namespace TransactionApp.Tests.Controllers
{
    public class ReportsControllerTests
    {
        private readonly TransactionContext _context;
        private readonly Mock<ILogger<ReportsController>> _mockLogger;
        private readonly ReportsController _controller;

        public ReportsControllerTests()
        {
            var options = new DbContextOptionsBuilder<TransactionContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new TransactionContext(options);
            _mockLogger = new Mock<ILogger<ReportsController>>();
            _controller = new ReportsController(_context, _mockLogger.Object);

            SeedTestData();
        }

        private void SeedTestData()
        {
            _context.Transactions.AddRange(
                new Transaction
                {
                    TransactionId = "1",
                    ProductName = "Product 1",
                    QuantitySold = 10,
                    PricePerUnit = 10m,
                    TotalSaleAmount = 100m,
                    TransactionDate = DateTime.UtcNow,
                    Region = "North"
                },
                new Transaction
                {
                    TransactionId = "2",
                    ProductName = "Product 2",
                    QuantitySold = 5,
                    PricePerUnit = 20m,
                    TotalSaleAmount = 100m,
                    TransactionDate = DateTime.UtcNow,
                    Region = "South"
                }
            );
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetTotalSales_ReturnsCorrectAmount()
        {

            var startDate = DateTime.UtcNow.AddDays(-1);
            var endDate = DateTime.UtcNow.AddDays(1);


            var result = await _controller.GetTotalSales(startDate, endDate, null, null);


            var okResult = Assert.IsType<OkObjectResult>(result);
            dynamic value = okResult.Value;
            Assert.Equal(200m, value.totalSales);
        }
    }
}