using TransactionApp.Models;
using TransactionApp.Data;
using Microsoft.EntityFrameworkCore;

namespace TransactionApp.Services
{
    public class DataService : IDataService
    {
        private readonly TransactionContext _context;
        private readonly IExternalDataService _externalDataService;

        public DataService(TransactionContext context, IExternalDataService externalDataService)
        {
            _context = context;
            _externalDataService = externalDataService;
        }

        public async Task ProcessTransactionsAsync(DateTime startDate, DateTime endDate)
        {
            var transactions = await _externalDataService.FetchTransactionsAsync(startDate, endDate);

            foreach (var transaction in transactions)
            {
                var exists = await _context.Transactions
                    .AnyAsync(t => t.TransactionId == transaction.TransactionId);

                if (!exists)
                {
                    await _context.Transactions.AddAsync(transaction);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}