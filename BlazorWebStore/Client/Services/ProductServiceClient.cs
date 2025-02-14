using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class ProductServiceClient(HttpClient httpClient)
    {
        public async Task<List<ProductViewModel>> GetAllProductAsync()
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
    }
}
