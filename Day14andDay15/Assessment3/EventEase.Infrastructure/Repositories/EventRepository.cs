using EventEase.Core.Entities;
using EventEase.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventEase.Repository.Implementations
{
    public class EventRepository : IEventRepository
    {
        private readonly List<Event> _events = new();
        private int _nextId = 1;

        public void Add(Event entity)
        {
            entity.Id = _nextId++;
            _events.Add(entity);
        }

        public void Update(Event entity)
        {
            var existing = _events.FirstOrDefault(e => e.Id == entity.Id);
            if (existing != null)
            {
                existing.Title = entity.Title;
                existing.Description = entity.Description;
                existing.Date = entity.Date;
                existing.Location = entity.Location;
            }
        }

        public void Delete(int id)
        {
            var evt = _events.FirstOrDefault(e => e.Id == id);
            if (evt != null)
            {
                _events.Remove(evt);
            }
        }

        public List<Event> GetAll()
        {
            return _events;
        }

        public Event? GetById(int id)
        {
            return _events.FirstOrDefault(e => e.Id == id);
        }

        public void SaveChanges()
        {
            
        }
    }
}
