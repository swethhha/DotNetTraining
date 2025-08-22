using EventEase.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventEase.Core.Interfaces
{
    public interface IEventService
    {
        // ----------------- SYNC -----------------
        int AddEvent(EventRequestDTO request);
        EventResponseDTO? GetEventById(int id);
        IEnumerable<EventResponseDTO> GetAllEvents();
        void UpdateEvent(int id, EventRequestDTO request);
        void DeleteEvent(int id);

        // ----------------- ASYNC -----------------
        Task<int> AddEventAsync(EventRequestDTO request);
        Task<EventResponseDTO?> GetEventByIdAsync(int id);
        Task<IEnumerable<EventResponseDTO>> GetAllEventsAsync();
        Task UpdateEventAsync(int id, EventRequestDTO request);
        Task DeleteEventAsync(int id);
    }
}
