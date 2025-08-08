using EventEase.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventEase.Core.Interfaces
{
    public interface IEventService
    {
        void AddEvent(EventRequestDTO eventRequest);
        void UpdateEvent(int id, EventRequestDTO eventRequest);
        void DeleteEvent(int id);
        List<EventResponseDTO> GetAllEvents();
        EventResponseDTO? GetEventById(int id);
    }
}
