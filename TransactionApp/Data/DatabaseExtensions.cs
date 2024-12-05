using Microsoft.EntityFrameworkCore;

namespace TransactionApp.Data
{
    public static class DatabaseExtensions
    {
        public static void EnsureDatabaseCreated(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var context = serviceProvider.GetRequiredService<TransactionContext>();
            context.Database.Migrate();
        }
    }
}
