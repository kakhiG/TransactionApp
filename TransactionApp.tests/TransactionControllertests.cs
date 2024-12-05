using Microsoft.AspNetCore.Mvc;
using Moq;
using TransactionApp.Controllers;
using TransactionApp.Services;
using Xunit;

namespace TransactionApp.Tests.Controllers
{
    public class TransactionsControllerTests
    {
        private readonly Mock<IDataService> _mockDataService;
        private readonly TransactionsController _controller;

        public TransactionsControllerTests()
        {
            _mockDataService = new Mock<IDataService>();
            _controller = new TransactionsController(_mockDataService.Object);
        }

        [Fact]
        public async Task FetchTransactions_ReturnsOkResult()
        {

            var startDate = DateTime.UtcNow.Date;
            var endDate = startDate.AddDays(1);

            _mockDataService
                .Setup(x => x.ProcessTransactionsAsync(startDate, endDate))
                .Returns(Task.CompletedTask);

            var result = await _controller.FetchTransactions(startDate, endDate);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}