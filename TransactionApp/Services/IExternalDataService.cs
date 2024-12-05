using TransactionApp.Models;

namespace TransactionApp.Services
{
    public interface IExternalDataService
    {
        Task<IEnumerable<Transaction>> FetchTransactionsAsync(DateTime startDate, DateTime endDate);
    }
}