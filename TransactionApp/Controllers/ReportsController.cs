using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TransactionApp.Data;
using TransactionApp.Models;

namespace TransactionApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly TransactionContext _context;
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(TransactionContext context, ILogger<ReportsController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet("total-sales")]
        public async Task<IActionResult> GetTotalSales(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            [FromQuery] string? region = null,
            [FromQuery] string? productName = null)
        {
            var query = _context.Transactions.AsQueryable();
            _logger.LogInformation($"Querying sales from {startDate} to {endDate}");

            if (!string.IsNullOrEmpty(region))
            {
                query = query.Where(t => t.Region == region);
                _logger.LogInformation($"Filtering by region: {region}");
            }

            if (!string.IsNullOrEmpty(productName))
            {
                query = query.Where(t => t.ProductName == productName);
                _logger.LogInformation($"Filtering by product: {productName}");
            }

            query = query.Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate);

            var totalSales = await query.SumAsync(t => t.TotalSaleAmount);
            _logger.LogInformation($"Total sales: {totalSales}");

            return Ok(totalSales);
        }

        [HttpGet("average-sale-per-product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAverageSalePerProduct(
       [FromQuery] DateTime startDate,
       [FromQuery] DateTime endDate,
       [FromQuery] string? region = null)
        {
            var query = _context.Transactions.AsQueryable();

            if (!string.IsNullOrEmpty(region))
            {
                query = query.Where(t => t.Region == region);
            }

            query = query.Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate);

            var averageSales = await query
                .GroupBy(t => t.ProductName)
                .Select(g => new
                {
                    ProductName = g.Key,
                    AverageSale = g.Average(t => t.TotalSaleAmount)
                })
                .ToListAsync();

            return Ok(averageSales);
        }
        [HttpGet("sales-summary-by-region")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSalesSummaryByRegion(
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        [FromQuery] string? productName = null)
        {
            var query = _context.Transactions.AsQueryable();

            if (!string.IsNullOrEmpty(productName))
            {
                query = query.Where(t => t.ProductName == productName);
            }

            query = query.Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate);

            var salesSummary = await query
                .GroupBy(t => t.Region)
                .Select(g => new
                {
                    Region = g.Key,
                    TotalSales = g.Sum(t => t.TotalSaleAmount)
                })
                .ToListAsync();

            return Ok(salesSummary);
        }
    }
}