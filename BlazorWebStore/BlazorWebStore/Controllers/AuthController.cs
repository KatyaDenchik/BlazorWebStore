using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        [HttpPost("loginAsAdmin")]
        public async Task<IActionResult> LoginAsAdmin([FromBody] UserLoginDTO loginDto)
        {
            var user = await userService.AuthenticateUserAsync(loginDto.Email, loginDto.Password);

            if (user == null)
                return Unauthorized(new { Message = "Невірний email или пароль" });

            if (user.Role != Shared.Enums.UserRole.Admin)
                return Unauthorized(new { Message = "Юзер не є адміном" });

            var token = jwtService.GenerateToken(user);
            await SetCookies(user);
            return Ok(new { Token = token, User = user });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO loginDto)
        {
            var user = await userService.AuthenticateUserAsync(loginDto.Email, loginDto.Password);
            if (user == null)
                return Unauthorized(new { Message = "Невірний email или пароль" });
            await SetCookies(user);

            return Ok(user);
        }

        private async Task SetCookies(UserDTO user)
        {
            var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.Email),
                            new Claim(ClaimTypes.Role, user.Role.ToString())
                        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                });
        }
    }
}
