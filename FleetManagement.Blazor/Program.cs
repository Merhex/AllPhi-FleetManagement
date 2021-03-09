using Blazored.LocalStorage;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using FleetManagement.Blazor.Services;
using IdentityModel.Client;
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

            builder.Services.ConfigureServices();
            builder.RootComponents.Add<App>("#app");

            await builder.Services.ProvideHttpClientWithIdentityServerAccessToken(builder);

            var host = builder.Build();

            await host.RunAsync();
        }
    }
}
