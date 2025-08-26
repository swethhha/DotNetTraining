using HostelManagement.Core.DTOs;
using HostelManagement.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HostelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "rooms")]
    //[ApiExplorerSettings(GroupName = "Hostel Management API")]

    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null) return NotFound();
            return Ok(room);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> AddRoom([FromBody] RoomRequestDTO roomDto)
        {
            await _roomService.AddRoomAsync(roomDto);
            return Ok(new { Message = "Room Created Successfully" });
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomRequestDTO roomDto)
        {
            await _roomService.UpdateRoomAsync(id, roomDto);
            return Ok(new { Message = "Room Updated Successfully" });
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            await _roomService.DeleteRoomAsync(id);
            return Ok(new { Message = "Room Deleted Successfully" });
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
