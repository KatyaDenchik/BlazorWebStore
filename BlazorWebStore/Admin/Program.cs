using Admin.Helpers;
using Admin.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Shared.Interfaces;

namespace Admin
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddScoped(sp =>
            {
                var handler = new AuthTokenHandler(sp.GetRequiredService<IJSRuntime>())
                {
                    InnerHandler = new HttpClientHandler()
                };
                return new HttpClient(handler)
                {
                    BaseAddress = new Uri("https://localhost:7110"),
                    DefaultRequestHeaders = { { "Accept", "application/json" } }
                };
            });
            builder.Services.AddScoped<IAuthService , JWTAuthService>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<AuthTokenHandler>();

            await builder.Build().RunAsync();
        }
    }
}
