using HostelManagement.Core.DTOs;
using HostelManagement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public ActionResult<List<StudentResponseDTO>> GetAll()
        {
            return Ok(_studentService.GetAllStudents());
        }

        [HttpGet("{id}")]
        public ActionResult<StudentResponseDTO> GetById(int id)
        {
            var student = _studentService.GetStudentById(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public IActionResult Create(StudentRequestDTO studentDto)
        {
            _studentService.AddStudent(studentDto);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, StudentRequestDTO studentDto)
        {
            var existing = _studentService.GetStudentById(id);
            if (existing == null) return NotFound();

            _studentService.UpdateStudent(id, studentDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _studentService.GetStudentById(id);
            if (existing == null) return NotFound();

            _studentService.DeleteStudent(id);
            return NoContent();
        }
    }
}
