using HostelManagement.Core.DTOs;
using HostelManagement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null) return NotFound();
            return Ok(room);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom([FromBody] RoomRequestDTO roomDto)
        {
            await _roomService.AddRoomAsync(roomDto);
            return Ok(new { Message = "Room Created Successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomRequestDTO roomDto)
        {
            await _roomService.UpdateRoomAsync(id, roomDto);
            return Ok(new { Message = "Room Updated Successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            await _roomService.DeleteRoomAsync(id);
            return Ok(new { Message = "Room Deleted Successfully" });
        }
    }
}
