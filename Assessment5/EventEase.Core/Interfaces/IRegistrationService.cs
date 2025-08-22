using EventEase.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventEase.Core.Interfaces
{
    public interface IRegistrationService
    {
        // ----------------- SYNC -----------------
        int AddRegistration(RegistrationRequestDTO request);
        RegistrationResponseDTO? GetRegistrationById(int id);
        IEnumerable<RegistrationResponseDTO> GetAllRegistrations();
        void UpdateRegistration(int id, RegistrationRequestDTO request);  // <-- add this
        void DeleteRegistration(int id);

        // ----------------- ASYNC -----------------
        Task<int> AddRegistrationAsync(RegistrationRequestDTO request);
        Task<RegistrationResponseDTO?> GetRegistrationByIdAsync(int id);
        Task<IEnumerable<RegistrationResponseDTO>> GetAllRegistrationsAsync();
        Task UpdateRegistrationAsync(int id, RegistrationRequestDTO request);  // <-- add this
        Task DeleteRegistrationAsync(int id);
    }
}
