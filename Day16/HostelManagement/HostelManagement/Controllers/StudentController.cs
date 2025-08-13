using HostelManagement.Core.DTOs;
using HostelManagement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpPost]
    public IActionResult Create(StudentRequestDTO studentDto)
    {
        try
        {
            _studentService.AddStudent(studentDto);
            return Ok("Student added successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var students = _studentService.GetAllStudents();
        return Ok(students);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var student = _studentService.GetStudentById(id);
        if (student == null)
            return NotFound();
        return Ok(student);
    }


}
