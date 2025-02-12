using AutoMapper;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository.Interfaces;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class UserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly PasswordService passwordService;

        public UserService(IUserRepository userRepository, IMapper mapper, PasswordService passwordService)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.passwordService = passwordService;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await userRepository.GetAsync();
            return mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = (await userRepository.GetAsync(u => u.Id == id)).FirstOrDefault();
            return mapper.Map<UserDTO>(user);
        }

        public async Task AddOrUpdateUserAsync(UserDTO userDto)
        {
            var userEntity = mapper.Map<UserEntity>(userDto);

            if (!string.IsNullOrEmpty(userDto.Password))
            {
                userEntity.PasswordHash = passwordService.HashPassword(userDto.Password);
            }

            await userRepository.CreateAsync(userEntity);
        }

        public async Task DeleteUserAsync(int id)
        {
            await userRepository.DeleteByIdAsync(id);
        }

        public async Task<UserDTO> AuthenticateUserAsync(string email, string password)
        {
            var user = (await userRepository.GetAsync(u => u.Email == email)).FirstOrDefault();

            if (user == null || !passwordService.VerifyPassword(password, user.PasswordHash))
                return null;

            return new UserDTO
            {
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}
