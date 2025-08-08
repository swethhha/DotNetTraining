using EventEase.Core.DTOs;
using EventEase.Core.Entities;
using EventEase.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventEase.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void AddUser(UserRequestDTO userRequest)
        {
            var userEntity = new User
            {
                Name = userRequest.Name,
                Email = userRequest.Email,
            };
            _userRepository.Add(userEntity);
            _userRepository.SaveChanges();
        }
        public void UpdateUser(int id, UserRequestDTO userRequest)
        {
            var existingUser = _userRepository.GetById(id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            existingUser.Name = userRequest.Name;
            existingUser.Email = userRequest.Email;
            _userRepository.Update(existingUser);
            _userRepository.SaveChanges();
        }
        public void DeleteUser(int id)
        {
            var existingUser = _userRepository.GetById(id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            _userRepository.Delete(id);
            _userRepository.SaveChanges();
        }
        public List<UserResponseDTO> GetAllUsers()
        {
            var users = _userRepository.GetAll();
            return users.Select(u => new UserResponseDTO
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email
            }).ToList();
        }
        public UserResponseDTO? GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null) return null;
            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
