using BankPro.Core.DTOs;
using BankPro.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BankPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // 1️⃣ GET: api/transaction
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            return Ok(transactions);
        }

        // 2️⃣ GET: api/transaction/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null)
                return NotFound(new { message = "Transaction not found." });

            return Ok(transaction);
        }

        // 3️⃣ GET: api/transaction/type/{type}
        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetByType(string type)
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            var filtered = transactions.Where(t => t.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
            return Ok(filtered);
        }

        // 4️⃣ GET: api/transaction/dates?from=yyyy-MM-dd&to=yyyy-MM-dd
        [HttpGet("dates")]
        public async Task<IActionResult> GetByDateRange([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            var filtered = transactions.Where(t => t.Date.Date >= from.Date && t.Date.Date <= to.Date);
            return Ok(filtered);
        }
    }
}
