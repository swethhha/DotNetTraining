using BankPro.Core.DTOs;
using BankPro.Core.Entities;
using BankPro.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankPro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        // GET: api/Account
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAll()
        {
            var accounts = await _service.GetAllAsync();
            return Ok(accounts);
        }

        // GET: api/Account/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetById(int id)
        {
            var account = await _service.GetByIdAsync(id);
            if (account == null) return NotFound();
            return Ok(account);
        }

        // POST: api/Account
        [HttpPost]
        public async Task<ActionResult> Create(AccountRequestDTO dto)
        {
            await _service.AddAccountAsync(dto);
            // Since Id is not returned, we just return 201 with the DTO
            return Created(string.Empty, dto);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, AccountRequestDTO dto)
        {
            await _service.UpdateAccountAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteAccountAsync(id);
            return NoContent();
        }

        [HttpPost("transfer")]
        public async Task<ActionResult> Transfer([FromQuery] string fromAccount, [FromQuery] string toAccount, [FromQuery] decimal amount)
        {
            try
            {
                await _service.TransferAsync(fromAccount, toAccount, amount);
                return Ok(new { message = "Transfer successful" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
