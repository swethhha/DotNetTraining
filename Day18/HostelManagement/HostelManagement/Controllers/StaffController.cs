using HostelManagement.Core.DTOs;
using HostelManagement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStaff()
        {
            var staffList = await _staffService.GetAllStaffAsync();
            return Ok(staffList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffById(int id)
        {
            var staff = await _staffService.GetStaffByIdAsync(id);
            if (staff == null) return NotFound();
            return Ok(staff);
        }

        [HttpPost]
        public async Task<IActionResult> AddStaff([FromBody] StaffRequestDTO staffDto)
        {
            await _staffService.AddStaffAsync(staffDto);
            return Ok(new { Message = "Staff Created Successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(int id, [FromBody] StaffRequestDTO staffDto)
        {
            await _staffService.UpdateStaffAsync(id, staffDto);
            return Ok(new { Message = "Staff Updated Successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            await _staffService.DeleteStaffAsync(id);
            return Ok(new { Message = "Staff Deleted Successfully" });
        }
    }
}
