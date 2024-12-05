using Microsoft.AspNetCore.Mvc;
using TransactionApp.Services;

namespace TransactionApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IDataService _dataService;

        public TransactionsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpPost("fetch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> FetchTransactions([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                await _dataService.ProcessTransactionsAsync(startDate, endDate);
                return Ok("Transactions processed successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}