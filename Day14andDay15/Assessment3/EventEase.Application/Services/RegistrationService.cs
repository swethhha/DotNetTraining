using EventEase.Core.DTOs;
using EventEase.Core.Entities;
using EventEase.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventEase.Application.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;
        public RegistrationService(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }
        public void AddRegistration(RegistrationRequestDTO registrationRequest)
        {
            var registrationEntity = new Registration
            {
                UserId = registrationRequest.UserId,
                EventId = registrationRequest.EventId,
                RegistrationDate = DateTime.Now
            };
            _registrationRepository.Add(registrationEntity);
            _registrationRepository.SaveChanges();
        }
        public void UpdateRegistration(int id, RegistrationRequestDTO registrationRequest)
        {
            var existingRegistration = _registrationRepository.GetById(id);
            if (existingRegistration == null)
            {
                throw new KeyNotFoundException("Registration not found");
            }
            existingRegistration.UserId = registrationRequest.UserId;
            existingRegistration.EventId = registrationRequest.EventId;
            _registrationRepository.Update(existingRegistration);
            _registrationRepository.SaveChanges();
        }
        public void DeleteRegistration(int id)
        {
            var existingRegistration = _registrationRepository.GetById(id);
            if (existingRegistration == null)
            {
                throw new KeyNotFoundException("Registration not found");
            }
            _registrationRepository.Delete(id);
            _registrationRepository.SaveChanges();
        }
        public List<RegistrationResponseDTO> GetAllRegistrations()
        {
            var registrations = _registrationRepository.GetAll();
            return registrations.Select(r => new RegistrationResponseDTO
            {
                Id = r.Id,
                UserId = r.UserId,
                EventId = r.EventId,
                RegistrationDate = r.RegistrationDate
            }).ToList();
        }
        public RegistrationResponseDTO? GetRegistrationById(int id)
        {
            var registration = _registrationRepository.GetById(id);
            if (registration == null)
            {
                return null;
            }
            return new RegistrationResponseDTO
            {
                Id = registration.Id,
                UserId = registration.UserId,
                EventId = registration.EventId,
                RegistrationDate = registration.RegistrationDate
            };
        }
    }
}
