using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService userService;
        private readonly JwtService jwtService;

        public AuthController(UserService userService, JwtService jwtService)
        {
            this.userService = userService;
            this.jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO loginDto)
        {
            var user = await userService.AuthenticateUserAsync(loginDto.Email, loginDto.Password);

            if (user == null)
                return Unauthorized(new { Message = "Невірний email или пароль" });

            var token = jwtService.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }
}
