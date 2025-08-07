using System.Collections.Generic;
using BugTracker.Core.DTOs;

namespace BugTracker.Core.Interfaces
{
    public interface IUserService
    {
        void AddUser(UserRequestDTO userRequest);
        void UpdateUser(UserRequestDTO userRequest);
        void DeleteUser(int id);
        UserResponseDTO GetUserById(int id);
        List<UserResponseDTO> GetAllUsers();
    }
}
