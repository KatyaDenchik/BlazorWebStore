using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Shared.Models;

namespace Admin.Services
{
    public class ProductService(HttpClient httpClient)
    {
        public async Task<List<ProductViewModel>> GetAllProductAsync( )
        {
            var response = await httpClient.GetAsync("api/products");
            if (!response.IsSuccessStatusCode) return new List<ProductViewModel>();
            var result = await response.Content.ReadFromJsonAsync<List<ProductViewModel>>();
            return result;
        }

        public async Task<ProductViewModel> GetProductByIdAsync(int id)
        {
            var response = await httpClient.GetAsync($"api/products/{id}");
            if (!response.IsSuccessStatusCode) return new ProductViewModel();
            var result = await response.Content.ReadFromJsonAsync<ProductViewModel>();
            return result;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var response = await httpClient.DeleteAsync($"api/products/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateAsync(ProductViewModel product)
        {
            var response = await httpClient.PostAsJsonAsync("api/products", product);
            return response.IsSuccessStatusCode;
        }
    }
}
