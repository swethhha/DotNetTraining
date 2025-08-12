//using BugTrack.Core.DTOs;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Linq;

//namespace BugTrack.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProjectController : ControllerBase
//    {
//        // In-memory sample data list (simulates DB)
//        //private static List<ProjectResponseDTO> _projects = new List<ProjectResponseDTO>
//        //{
//        //    new ProjectResponseDTO { ProjectId = 1, ProjectName = "Default Project", Description = "This is a default project description." },
//        //    new ProjectResponseDTO { ProjectId = 2, ProjectName = "Bug Tracker Upgrade", Description = "Upgrade bug tracking system to support tagging and search." }
//        //};

//        //// GET: api/project
//        //[HttpGet]
//        //public IActionResult GetAllProjects()
//        //{
//        //    return Ok(_projects);
//        //}

//        //// GET: api/project/{id}
//        //[HttpGet("{id}")]
//        //public IActionResult GetProjectById(int id)
//        //{
//        //    var project = _projects.FirstOrDefault(p => p.ProjectId == id);
//        //    if (project == null)
//        //        return NotFound(new { message = $"Project with ID {id} not found." });

//        //    return Ok(project);
//        //}

//        //// POST: api/project
//        //[HttpPost]
//        //public IActionResult AddProject([FromBody] ProjectResponseDTO project)
//        //{
//        //    if (project == null || string.IsNullOrWhiteSpace(project.ProjectName))
//        //        return BadRequest(new { message = "Project data is invalid." });

//        //    project.ProjectId = _projects.Any() ? _projects.Max(p => p.ProjectId) + 1 : 1;
//        //    _projects.Add(project);

//        //    return CreatedAtAction(nameof(GetProjectById), new { id = project.ProjectId }, project);
//        //}

//        //// PUT: api/project/{id}
//        //[HttpPut("{id}")]
//        //public IActionResult UpdateProject(int id, [FromBody] ProjectResponseDTO project)
//        //{
//        //    if (project == null || string.IsNullOrWhiteSpace(project.ProjectName))
//        //        return BadRequest(new { message = "Project data is invalid." });

//        //    var existingProject = _projects.FirstOrDefault(p => p.ProjectId == id);
//        //    if (existingProject == null)
//        //        return NotFound(new { message = $"Project with ID {id} not found." });

//        //    existingProject.ProjectName = project.ProjectName;
//        //    existingProject.Description = project.Description;

//        //    return NoContent(); // 204
//        //}

//        //// DELETE: api/project/{id}
//        //[HttpDelete("{id}")]
//        //public IActionResult DeleteProject(int id)
//        //{
//        //    var project = _projects.FirstOrDefault(p => p.ProjectId == id);
//        //    if (project == null)
//        //        return NotFound(new { message = $"Project with ID {id} not found." });

//        //    _projects.Remove(project);

//        //    return NoContent(); // 204
//        //}
//    }
//}
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
    }
}
