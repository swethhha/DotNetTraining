using BankPro.Core.DTOs;
using BankPro.Core.Entities;
using BankPro.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: api/Account
        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAsync();
            return Ok(accounts);
        }

        // GET: api/Account/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var account = await _accountService.GetByIdAsync(id);
            if (account == null)
                return NotFound(new { message = "Account not found" });

            return Ok(account);
        }

        // POST: api/Account
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRequestDTO accountDto)
        {
            await _accountService.AddAccountAsync(accountDto);
            return Ok(new { message = "Account created successfully" });
        }

        // PUT: api/Account/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] AccountRequestDTO accountDto)
        {
            await _accountService.UpdateAccountAsync(id, accountDto);
            return Ok(new { message = "Account updated successfully" });
        }

        // DELETE: api/Account/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            await _accountService.DeleteAccountAsync(id);
            return Ok(new { message = "Account deleted successfully" });
        }

        // POST: api/Account/deposit
        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromQuery] string accountNumber, [FromQuery] decimal amount)
        {
            await _accountService.DepositAsync(accountNumber, amount);
            return Ok(new { message = "Deposit successful" });
        }

        // POST: api/Account/withdraw
        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromQuery] string accountNumber, [FromQuery] decimal amount)
        {
            await _accountService.WithdrawAsync(accountNumber, amount);
            return Ok(new { message = "Withdrawal successful" });
        }

        // POST: api/Account/transfer
        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromQuery] string fromAccount, [FromQuery] string toAccount, [FromQuery] decimal amount)
        {
            await _accountService.TransferAsync(fromAccount, toAccount, amount);
            return Ok(new { message = "Transfer successful" });
        }
    }
}
