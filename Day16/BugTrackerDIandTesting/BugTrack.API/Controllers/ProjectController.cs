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
    }
}
