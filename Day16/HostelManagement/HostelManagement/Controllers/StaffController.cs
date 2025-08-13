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
        public ActionResult<List<StaffResponseDTO>> GetAll()
        {
            return Ok(_staffService.GetAllStaff());
        }

        [HttpGet("{id}")]
        public ActionResult<StaffResponseDTO> GetById(int id)
        {
            var staff = _staffService.GetStaffById(id);
            if (staff == null) return NotFound();
            return Ok(staff);
        }

        [HttpPost]
        public IActionResult Create(StaffRequestDTO staffDto)
        {
            _staffService.AddStaff(staffDto);
            return Ok("Staff created successfully");
        }


    }
}
