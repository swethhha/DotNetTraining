using HostelManagement.Core.DTOs;
using HostelManagement.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HostelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [ApiExplorerSettings(GroupName = "students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> AddStudent([FromBody] StudentRequestDTO studentDto)
        {
            await _studentService.AddStudentAsync(studentDto);
            return Ok(new { Message = "Student Created Successfully" });
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentRequestDTO studentDto)
        {
            await _studentService.UpdateStudentAsync(id, studentDto);
            return Ok(new { Message = "Student Updated Successfully" });
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return Ok(new { Message = "Student Deleted Successfully" });
        }
        [HttpGet("me")]
        [Authorize]
        public ActionResult GetMyClaims()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            return Ok(new { username, role });
        }
    }
}
