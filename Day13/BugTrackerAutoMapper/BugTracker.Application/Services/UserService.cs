using AutoMapper;
using BugTracker.Core.DTOs;
using BugTracker.Core.Entities;
using BugTracker.Core.Interfaces;
using System.Collections.Generic;

namespace BugTracker.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public void AddUser(UserRequestDTO userRequest)
        {
            var user = _mapper.Map<User>(userRequest);
            _userRepository.Add(user);
        }

        public void UpdateUser(UserRequestDTO userRequest)
        {
            var user = _mapper.Map<User>(userRequest);
            _userRepository.Update(user);
        }

        public void DeleteUser(int id)
        {
            _userRepository.Delete(id);
        }

        public UserResponseDTO GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            return _mapper.Map<UserResponseDTO>(user);
        }

        public List<UserResponseDTO> GetAllUsers()
        {
            var users = _userRepository.GetAll();
            return _mapper.Map<List<UserResponseDTO>>(users);
        }
    }
}
