using HostelManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelManagement.Core.Interfaces
{
    public interface IRoomService 
    {
            void AddRoom(RoomRequestDTO roomDto);
            void UpdateRoom(int id, RoomRequestDTO roomDto);
            void DeleteRoom(int id);
            RoomResponseDTO? GetRoomById(int id);
            
            List<RoomResponseDTO> GetAllRooms();
    }
}
