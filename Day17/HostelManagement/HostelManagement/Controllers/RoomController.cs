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
        public ActionResult<List<RoomResponseDTO>> GetAll()
        {
            return Ok(_roomService.GetAllRooms());
        }

        [HttpGet("{id}")]
        public ActionResult<RoomResponseDTO> GetById(int id)
        {
            var room = _roomService.GetRoomById(id);
            if (room == null) return NotFound();
            return Ok(room);
        }

        [HttpPost]
        public IActionResult AddRoom(RoomRequestDTO roomDto)
        {
            _roomService.AddRoom(roomDto);
            return Ok("Room added successfully.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, RoomRequestDTO roomDto)
        {
            var existing = _roomService.GetRoomById(id);
            if (existing == null) return NotFound();

            _roomService.UpdateRoom(id, roomDto);
            return Ok("Room updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _roomService.GetRoomById(id);
            if (existing == null) return NotFound();

            _roomService.DeleteRoom(id);
            return Ok("Room deleted successfully");
        }
    }
}
