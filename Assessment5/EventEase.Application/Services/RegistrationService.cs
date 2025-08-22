using EventEase.Core.DTOs;
using EventEase.Core.Entities;
using EventEase.Core.Exceptions;
using EventEase.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ValidationException = EventEase.Core.Exceptions.ValidationException;

namespace EventEase.Application.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;

        public RegistrationService(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        // ----------------- SYNC -----------------
        public int AddRegistration(RegistrationRequestDTO request)
        {
            var reg = new Registration { UserId = request.UserId, EventId = request.EventId };
            _registrationRepository.Add(reg);
            return reg.Id;
        }

        public RegistrationResponseDTO? GetRegistrationById(int id)
        {
            var reg = _registrationRepository.GetById(id);
            if (reg == null) throw new NotFoundException($"Registration with ID {id} not found.");
            return MapToResponseDTO(reg);
        }

        public IEnumerable<RegistrationResponseDTO> GetAllRegistrations()
        {
            return _registrationRepository.GetAll().Select(MapToResponseDTO);
        }

        public void UpdateRegistration(int id, RegistrationRequestDTO request)
        {
            var reg = _registrationRepository.GetById(id);
            if (reg == null) throw new NotFoundException($"Registration with ID {id} not found.");

            reg.UserId = request.UserId;
            reg.EventId = request.EventId;

            _registrationRepository.Update(reg);
        }

        public void DeleteRegistration(int id)
        {
            var reg = _registrationRepository.GetById(id);
            if (reg == null) throw new NotFoundException($"Registration with ID {id} not found.");
            _registrationRepository.Delete(id);
        }

        // ----------------- ASYNC -----------------
        public async Task<int> AddRegistrationAsync(RegistrationRequestDTO request)
        {
            var reg = new Registration { UserId = request.UserId, EventId = request.EventId };
            await _registrationRepository.AddAsync(reg);
            return reg.Id;
        }

        public async Task<RegistrationResponseDTO?> GetRegistrationByIdAsync(int id)
        {
            var reg = await _registrationRepository.GetByIdAsync(id);
            if (reg == null) throw new NotFoundException($"Registration with ID {id} not found.");
            return MapToResponseDTO(reg);
        }

        public async Task<IEnumerable<RegistrationResponseDTO>> GetAllRegistrationsAsync()
        {
            var regs = await _registrationRepository.GetAllAsync();
            return regs.Select(MapToResponseDTO);
        }

        public async Task UpdateRegistrationAsync(int id, RegistrationRequestDTO request)
        {
            var reg = await _registrationRepository.GetByIdAsync(id);
            if (reg == null) throw new NotFoundException($"Registration with ID {id} not found.");

            reg.UserId = request.UserId;
            reg.EventId = request.EventId;

            await _registrationRepository.UpdateAsync(reg);
        }

        public async Task DeleteRegistrationAsync(int id)
        {
            var reg = await _registrationRepository.GetByIdAsync(id);
            if (reg == null) throw new NotFoundException($"Registration with ID {id} not found.");
            await _registrationRepository.DeleteAsync(id);
        }

        // ----------------- Helper -----------------
        private RegistrationResponseDTO MapToResponseDTO(Registration reg)
        {
            return new RegistrationResponseDTO
            {
                Id = reg.Id,
                UserId = reg.UserId,
                EventId = reg.EventId
            };
        }
    }
}
