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
    public class CookiesAuthServices(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IJSRuntime jsRuntime) : IAuthService
    {
        public async Task<bool> LoginAsync(UserLoginDTO user)
        {
            var requestContent = JsonContent.Create(user);
            var response = await httpClient.PostAsync("api/auth/login", requestContent);
            if (!response.IsSuccessStatusCode) return false;
            var userDTO = await response.Content.ReadFromJsonAsync<UserDTO>();
            await jsRuntime.InvokeVoidAsync("localStorage.setItem", "userId", userDTO.Id.ToString());
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
