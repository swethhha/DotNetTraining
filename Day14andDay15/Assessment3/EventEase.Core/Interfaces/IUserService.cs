using EventEase.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventEase.Core.Interfaces
{
    public interface IUserService
    {
        void AddUser(UserRequestDTO userRequest);
        void UpdateUser(int id, UserRequestDTO userRequest);
        void DeleteUser(int id);
        List<UserResponseDTO> GetAllUsers();
        UserResponseDTO? GetUserById(int id);
    }
}
