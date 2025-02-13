using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http;
using Microsoft.JSInterop;
using Shared.DTOs;
using Shared.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class CookiesAuthServices(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : IAuthService
    {
        public async Task<bool> LoginAsync(UserLoginDTO user)
        {
            var requestContent = JsonContent.Create(user);
            var response = await httpClient.PostAsync("api/auth/login", requestContent);
            if (!response.IsSuccessStatusCode) return false;
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
        };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal,
                new AuthenticationProperties
                {
                    IsPersistent = true, 
                });

            return true;
    }

        public async Task<bool> RegisterAsync(RegisterModel user)
        {
            if (user.Password != user.ConfirmPassword) return false;
            var response = await httpClient.PostAsJsonAsync("api/users/register", user);
            return response.IsSuccessStatusCode;
        }
    }
}
