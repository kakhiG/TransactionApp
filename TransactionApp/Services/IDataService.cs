namespace TransactionApp.Services
{
    public interface IDataService
    {
        Task ProcessTransactionsAsync(DateTime startDate, DateTime endDate);
    }
}