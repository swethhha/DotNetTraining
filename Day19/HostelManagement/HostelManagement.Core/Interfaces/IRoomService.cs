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
            Task<List<RoomResponseDTO>> GetAllRoomsAsync();
            Task<RoomResponseDTO?> GetRoomByIdAsync(int id);
            Task AddRoomAsync(RoomRequestDTO roomRequest);
            Task UpdateRoomAsync(int id, RoomRequestDTO roomRequest);
            Task DeleteRoomAsync(int id);

    }
}
