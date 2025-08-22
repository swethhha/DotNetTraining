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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // ----------------- SYNC -----------------
        public int AddUser(UserRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ValidationException(new Dictionary<string, string[]>
                { { "Name", new[] { "Name is required." } } });

            var user = new User { Name = request.Name, Email = request.Email };
            _userRepository.Add(user);
            return user.Id;
        }

        public UserResponseDTO? GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null) throw new NotFoundException($"User with ID {id} not found.");
            return MapToResponseDTO(user);
        }

        public IEnumerable<UserResponseDTO> GetAllUsers()
        {
            return _userRepository.GetAll().Select(MapToResponseDTO);
        }

        public void UpdateUser(int id, UserRequestDTO request)
        {
            var user = _userRepository.GetById(id);
            if (user == null) throw new NotFoundException($"User with ID {id} not found.");

            user.Name = request.Name;
            user.Email = request.Email;
            _userRepository.Update(user);
        }

        public void DeleteUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null) throw new NotFoundException($"User with ID {id} not found.");
            _userRepository.Delete(id);
        }

        // ----------------- ASYNC -----------------
        public async Task<int> AddUserAsync(UserRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ValidationException(new Dictionary<string, string[]>
                { { "Name", new[] { "Name is required." } } });

            var user = new User { Name = request.Name, Email = request.Email };
            await _userRepository.AddAsync(user);
            return user.Id;
        }

        public async Task<UserResponseDTO?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new NotFoundException($"User with ID {id} not found.");
            return MapToResponseDTO(user);
        }

        public async Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(MapToResponseDTO);
        }

        public async Task UpdateUserAsync(int id, UserRequestDTO request)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new NotFoundException($"User with ID {id} not found.");

            user.Name = request.Name;
            user.Email = request.Email;
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new NotFoundException($"User with ID {id} not found.");
            await _userRepository.DeleteAsync(id);
        }

        private UserResponseDTO MapToResponseDTO(User user)
        {
            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
