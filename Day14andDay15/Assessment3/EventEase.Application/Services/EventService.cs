using EventEase.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventEase.Core.DTOs;
using EventEase.Core.Entities;
namespace EventEase.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public void AddEvent(EventRequestDTO eventDto)
        {

            var eventEntity = new Event
            {
                Title = eventDto.Title,
                Description = eventDto.Description,
                Date = eventDto.Date,
                Location = eventDto.Location
            };
            _eventRepository.Add(eventEntity);
            _eventRepository.SaveChanges();
        }
        public void UpdateEvent(int id, EventRequestDTO eventDto)
        {
            var existingEvent = _eventRepository.GetById(id);
            if (existingEvent == null)
            {
                throw new KeyNotFoundException("Event not found");
            }
            existingEvent.Title = eventDto.Title;
            existingEvent.Description = eventDto.Description;
            existingEvent.Date = eventDto.Date;
            existingEvent.Location = eventDto.Location;
            _eventRepository.Update(existingEvent);
            _eventRepository.SaveChanges();
        }
        public void DeleteEvent(int id)
        {
            var existingEvent = _eventRepository.GetById(id);
            if (existingEvent == null)
            {
                throw new KeyNotFoundException("Event not found");
            }
            _eventRepository.Delete(id);
            _eventRepository.SaveChanges();
        }
        public List<EventResponseDTO> GetAllEvents()
        {
            var events = _eventRepository.GetAll();
            return events.Select(e => new EventResponseDTO
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Date = e.Date,
                Location = e.Location
            }).ToList();
        }
        public EventResponseDTO? GetEventById(int id)
        {
            var existingEvent = _eventRepository.GetById(id);
            if (existingEvent == null)
            {
                return null;
            }
            return new EventResponseDTO
            {
                Id = existingEvent.Id,
                Title = existingEvent.Title,
                Description = existingEvent.Description,
                Date = existingEvent.Date,
                Location = existingEvent.Location
            };
        }
    }
}
