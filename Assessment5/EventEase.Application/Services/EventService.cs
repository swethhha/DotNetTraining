using EventEase.Core.DTOs;
using EventEase.Core.Entities;
using EventEase.Core.Exceptions;
using EventEase.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Force ValidationException to always use your custom one
using ValidationException = EventEase.Core.Exceptions.ValidationException;

namespace EventEase.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        // ----------------- SYNC -----------------
        public int AddEvent(EventRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                throw new ValidationException(new Dictionary<string, string[]>
                { { "Title", new[] { "Title is required." } } });

            var ev = new Event
            {
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                Date = request.Date
            };

            _eventRepository.Add(ev);
            return ev.Id;
        }

        public EventResponseDTO? GetEventById(int id)
        {
            var ev = _eventRepository.GetById(id);
            if (ev == null) throw new NotFoundException($"Event with ID {id} not found.");
            return MapToResponseDTO(ev);
        }

        public IEnumerable<EventResponseDTO> GetAllEvents()
        {
            return _eventRepository.GetAll().Select(MapToResponseDTO);
        }

        public void UpdateEvent(int id, EventRequestDTO request)
        {
            var ev = _eventRepository.GetById(id);
            if (ev == null) throw new NotFoundException($"Event with ID {id} not found.");

            if (string.IsNullOrWhiteSpace(request.Title))
                throw new ValidationException(new Dictionary<string, string[]>
                { { "Title", new[] { "Title is required." } } });

            ev.Title = request.Title;
            ev.Description = request.Description;
            ev.Location = request.Location;
            ev.Date = request.Date;

            _eventRepository.Update(ev);
        }

        public void DeleteEvent(int id)
        {
            var ev = _eventRepository.GetById(id);
            if (ev == null) throw new NotFoundException($"Event with ID {id} not found.");
            _eventRepository.Delete(id);
        }

        // ----------------- ASYNC -----------------
        public async Task<int> AddEventAsync(EventRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                throw new ValidationException(new Dictionary<string, string[]>
                { { "Title", new[] { "Title is required." } } });

            var ev = new Event
            {
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                Date = request.Date
            };

            await _eventRepository.AddAsync(ev);
            return ev.Id;
        }

        public async Task<EventResponseDTO?> GetEventByIdAsync(int id)
        {
            var ev = await _eventRepository.GetByIdAsync(id);
            if (ev == null) throw new NotFoundException($"Event with ID {id} not found.");
            return MapToResponseDTO(ev);
        }

        public async Task<IEnumerable<EventResponseDTO>> GetAllEventsAsync()
        {
            var events = await _eventRepository.GetAllAsync();
            return events.Select(MapToResponseDTO);
        }

        public async Task UpdateEventAsync(int id, EventRequestDTO request)
        {
            var ev = await _eventRepository.GetByIdAsync(id);
            if (ev == null) throw new NotFoundException($"Event with ID {id} not found.");

            if (string.IsNullOrWhiteSpace(request.Title))
                throw new ValidationException(new Dictionary<string, string[]>
                { { "Title", new[] { "Title is required." } } });

            ev.Title = request.Title;
            ev.Description = request.Description;
            ev.Location = request.Location;
            ev.Date = request.Date;

            await _eventRepository.UpdateAsync(ev);
        }

        public async Task DeleteEventAsync(int id)
        {
            var ev = await _eventRepository.GetByIdAsync(id);
            if (ev == null) throw new NotFoundException($"Event with ID {id} not found.");
            await _eventRepository.DeleteAsync(id);
        }

        // ----------------- Mapper -----------------
        private EventResponseDTO MapToResponseDTO(Event ev)
        {
            return new EventResponseDTO
            {
                Id = ev.Id,
                Title = ev.Title,
                Description = ev.Description,
                Location = ev.Location,
                Date = ev.Date
            };
        }
    }
}
