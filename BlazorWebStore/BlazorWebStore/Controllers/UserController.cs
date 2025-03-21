﻿using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            return await userService.GetAllUsersAsync();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await userService.GetUserByIdAsync(id);
            return user != null ? Ok(user) : NotFound();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO user)
        {
            await userService.AddOrUpdateUserAsync(user);
            return Ok(new { Message = "Користувач зареєстрований" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await userService.DeleteUserAsync(id);
            return Ok(new { Message = "Користувача видалено" });
        }
    }
}
