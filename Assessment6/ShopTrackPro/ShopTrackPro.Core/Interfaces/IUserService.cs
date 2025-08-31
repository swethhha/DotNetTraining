using ShopTrackPro.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTrackPro.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync();
        Task<UserResponseDTO?> GetUserByIdAsync(int id);
        Task<UserResponseDTO> CreateUserAsync(UserRequestDTO user);
        Task<UserResponseDTO> UpdateUserAsync(int id, UserRequestDTO user);
        Task<bool> DeleteUserAsync(int id);
    }
}
