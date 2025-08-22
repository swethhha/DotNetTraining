using EventEase.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventEase.Core.Interfaces
{
    public interface IUserService
    {
        // ----------------- SYNC -----------------
        int AddUser(UserRequestDTO request);
        UserResponseDTO? GetUserById(int id);
        IEnumerable<UserResponseDTO> GetAllUsers();
        void UpdateUser(int id, UserRequestDTO request);
        void DeleteUser(int id);

        // ----------------- ASYNC -----------------
        Task<int> AddUserAsync(UserRequestDTO request);
        Task<UserResponseDTO?> GetUserByIdAsync(int id);
        Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync();
        Task UpdateUserAsync(int id, UserRequestDTO request);
        Task DeleteUserAsync(int id);
    }
}
