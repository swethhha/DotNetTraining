using EventEase.Core.DTOs;
using EventEase.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EventEase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;

            // Sample users
            _userService.AddUser(new UserRequestDTO { Name = "Alice", Email = "alice@example.com" });
            _userService.AddUser(new UserRequestDTO { Name = "Bob", Email = "bob@example.com" });
        }

        [HttpGet("All")]
        public ActionResult<List<UserResponseDTO>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<UserResponseDTO> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound($"User with ID {id} not found.");
            return Ok(user);
        }

        [HttpPost("Add")]
        public IActionResult AddUser([FromBody] UserRequestDTO userDto)
        {
            _userService.AddUser(userDto);
            return Ok("User added successfully.");
        }
    }
}
