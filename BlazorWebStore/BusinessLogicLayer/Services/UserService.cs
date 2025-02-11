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

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
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
            await userRepository.CreateAsync(userEntity);
        }

        public async Task DeleteUserAsync(int id)
        {
            await userRepository.DeleteByIdAsync(id);
        }
    }
}
