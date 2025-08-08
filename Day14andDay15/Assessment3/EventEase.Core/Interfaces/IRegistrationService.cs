using EventEase.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventEase.Core.Interfaces
{
    public interface IRegistrationService
    {
        void AddRegistration(RegistrationRequestDTO registrationRequest);
        void UpdateRegistration(int id, RegistrationRequestDTO registrationRequest);
        void DeleteRegistration(int id);
        List<RegistrationResponseDTO> GetAllRegistrations();
        RegistrationResponseDTO? GetRegistrationById(int id);
    }
}
