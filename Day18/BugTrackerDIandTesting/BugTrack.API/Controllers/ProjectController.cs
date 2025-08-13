using System.Threading.Tasks;
using BugTrack.Core.DTOs;
using BugTrack.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BugTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _service;

        public ProjectController(IProjectService service)
        {
            _service = service;
        }

        // GET: api/project
        [HttpGet]
        public IActionResult GetAllProjects()
        {
            var projects = _service.GetAllProjects();
            if (projects == null || projects.Count == 0)
                return NotFound(new { message = "No projects found." });

            return Ok(projects);
        }

        // GET: api/project/{id}
        [HttpGet("{id}")]
        public IActionResult GetProjectById(int id)
        {
            var project = _service.GetProjectById(id);
            if (project == null)
                return NotFound(new { message = $"Project with ID {id} not found." });

            return Ok(project);
        }

        // POST: api/project
        [HttpPost]
        public IActionResult AddProject([FromBody] ProjectRequestDTO projectDto)
        {
            if (projectDto == null || string.IsNullOrWhiteSpace(projectDto.ProjectName))
                return BadRequest(new { message = "Invalid project data." });

            _service.AddProject(projectDto);
            return StatusCode(201, new { message = "Project created successfully." });
        }

        // PUT: api/project/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateProject(int id, [FromBody] ProjectRequestDTO projectDto)
        {
            if (projectDto == null || string.IsNullOrWhiteSpace(projectDto.ProjectName))
                return BadRequest(new { message = "Invalid project data." });

            var existingProject = _service.GetProjectById(id);
            if (existingProject == null)
                return NotFound(new { message = $"Project with ID {id} not found." });

            _service.UpdateProject(id, projectDto);
            return NoContent(); // 204 No Content
        }
        // DELETE: api/project/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            var existingProject = _service.GetProjectById(id);
            if (existingProject == null)
                return NotFound(new { message = $"Project with ID {id} not found." });
            _service.DeleteProject(id);
            return NoContent(); // 204 No Content
        }
        // Async methods
        [HttpGet("async")]
        public async Task<IActionResult> GetAllProjectsAsync()
        {
            var projects = await _service.GetAllProjectsAsync();
            if (projects == null || projects.Count == 0)
                return NotFound(new { message = "No projects found." });
            return Ok(projects);
        }
        [HttpGet("async/{id}")]
        public async Task<IActionResult> GetProjectByIdAsync(int id)
        {
            var project = await _service.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound(new { message = $"Project with ID {id} not found." });
            return Ok(project);
        }

        [HttpPost("async")]
        public async Task<IActionResult> AddProjectAsync([FromBody] ProjectRequestDTO projectDto)
        {
            if (projectDto == null || string.IsNullOrWhiteSpace(projectDto.ProjectName))
                return BadRequest(new { message = "Invalid project data." });
            await _service.AddProjectAsync(projectDto);
            return StatusCode(201, new { message = "Project created successfully." });
        }
        [HttpPut("async/{id}")]
        public async Task<IActionResult> UpdateProjectAsync(int id, [FromBody] ProjectRequestDTO projectDto)
        {
            if (projectDto == null || string.IsNullOrWhiteSpace(projectDto.ProjectName))
                return BadRequest(new { message = "Invalid project data." });
            var existingProject = await _service.GetProjectByIdAsync(id);
            if (existingProject == null)
                return NotFound(new { message = $"Project with ID {id} not found." });
            await _service.UpdateProjectAsync(id, projectDto);
            return NoContent(); // 204 No Content
        }
        [HttpDelete("async/{id}")]
        public async Task<IActionResult> DeleteProjectAsync(int id)
        {
            var existingProject = await _service.GetProjectByIdAsync(id);
            if (existingProject == null)
                return NotFound(new { message = $"Project with ID {id} not found." });
            await _service.DeleteProjectAsync(id);
            return NoContent(); // 204 No Content
        }

    }

}
