using AutoMapper;
using ShopTrackPro.Core.DTO;
using ShopTrackPro.Core.Entities;
using ShopTrackPro.Core.Exceptions;
using ShopTrackPro.Core.Interfaces;

namespace ShopTrackPro.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<UserResponseDTO> CreateUserAsync(UserRequestDTO userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.Name))
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Name", new[] { "User name is required." } }
                });

            if (string.IsNullOrWhiteSpace(userDto.Email))
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Email", new[] { "Email is required." } }
                });

            var user = _mapper.Map<User>(userDto);
            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();
            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<UserResponseDTO> UpdateUserAsync(int id, UserRequestDTO userDto)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null)
                throw new NotFoundException($"User with ID {id} not found.");

            if (string.IsNullOrWhiteSpace(userDto.Name))
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Name", new[] { "User name is required." } }
                });

            if (string.IsNullOrWhiteSpace(userDto.Email))
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Email", new[] { "Email is required." } }
                });

            _mapper.Map(userDto, user);
            _userRepo.Update(user);
            await _userRepo.SaveChangesAsync();
            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null)
                throw new NotFoundException($"User with ID {id} not found.");

            _userRepo.Delete(user);
            await _userRepo.SaveChangesAsync();
            return true;
        }

        public async Task<UserResponseDTO> GetUserByIdAsync(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null)
                throw new NotFoundException($"User with ID {id} not found.");

            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<UserResponseDTO>>(users);
        }
    }
}
