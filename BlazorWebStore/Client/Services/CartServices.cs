using Shared.DTOs;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class CartServices(HttpClient httpClient)
    {
        public async Task<List<CartItemDTO>> GetCartItemsAsync(int userId)
        {
            var response = await httpClient.GetAsync($"api/cart/{userId}");
            if (!response.IsSuccessStatusCode) return new List<CartItemDTO>();
            var result = await response.Content.ReadFromJsonAsync<List<CartItemDTO>>();
            return result;
        }

        public async Task<bool> AddToCartAsync(CartItemDTO cartItem)
        {
            var response = await httpClient.PostAsJsonAsync("api/cart", cartItem);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveFromCartAsync(int id)
        {
            var response = await httpClient.DeleteAsync($"api/cart/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ClearCartAsync(int userId)
        {
            var response = await httpClient.DeleteAsync($"api/cart/clear/{userId}");
            return response.IsSuccessStatusCode;
        }
    }
}
