using Microsoft.JSInterop;
using Shared.DTOs;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static Admin.Services.JWTAuthService;

namespace Admin.Services
{
    public class JWTAuthService(HttpClient httpClient, IJSRuntime jsRuntime) : IAuthService
    {
        public class LoginResponse
        {
            public string Token { get; set; }
        }
        public async Task<bool> LoginAsync(UserLoginDTO request)
        {
            var requestContent = JsonContent.Create(request);
            var response = await httpClient.PostAsync("api/auth/loginAsAdmin", requestContent);
            if (!response.IsSuccessStatusCode) return false;
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            await jsRuntime.InvokeVoidAsync("localStorage.setItem", "token", result.Token);
            return true;
        }
    }
}
