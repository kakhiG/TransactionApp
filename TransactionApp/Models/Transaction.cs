using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransactionApp.Models
{
    public class Transaction
    {
        public Transaction() { } 

        public string TransactionId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int QuantitySold { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal TotalSaleAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Region { get; set; } = string.Empty;
    }
}

