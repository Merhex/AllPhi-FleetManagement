using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using FleetManagement.Blazor.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FleetManagement.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services
              .AddBlazorise(options =>
              {
                  options.ChangeTextOnKeyPress = true;
              })
              .AddBootstrapProviders()
              .AddFontAwesomeIcons();

            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient 
            { 
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
            });

            builder.Services.AddScoped<IApiRequestService, ApiRequestService>();

            var host = builder.Build();

            await host.RunAsync();
        }
    }
}
