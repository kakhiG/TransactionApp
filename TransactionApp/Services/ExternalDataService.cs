using TransactionApp.Models;

namespace TransactionApp.Services
{
    public class ExternalDataService : IExternalDataService
    {
        public async Task<IEnumerable<Transaction>> FetchTransactionsAsync(DateTime startDate, DateTime endDate)
        {

            await Task.Delay(100);


            return new List<Transaction>
            {
                new Transaction
                {
                    TransactionId = Guid.NewGuid().ToString(),
                    ProductName = "Product 1",
                    QuantitySold = 10,
                    PricePerUnit = 9.25m,
                    TotalSaleAmount = 15.75m,
                    TransactionDate = DateTime.UtcNow,
                    Region = "North"
                },
                new Transaction
                {
                TransactionId = Guid.NewGuid().ToString(),
                ProductName = "Product 2",
                QuantitySold = 5,
                PricePerUnit = 19.75m,
                TotalSaleAmount = 28.95m,
                TransactionDate = DateTime.UtcNow,
                Region = "South"

                },
                new Transaction
            {
                TransactionId = Guid.NewGuid().ToString(),
                ProductName = "Product 3",
                QuantitySold = 25,
                PricePerUnit = 27.98m,
                TotalSaleAmount = 52.85m,
                TransactionDate = DateTime.UtcNow,
                Region = "East"
            },
                new Transaction
            {
                TransactionId = Guid.NewGuid().ToString(),
                ProductName = " Product 4",
                QuantitySold = 85,
                PricePerUnit = 89.9m,
                TotalSaleAmount = 75.48m,
                TransactionDate = DateTime.UtcNow,
                Region = "West"
            }

            };
        }
    }
}