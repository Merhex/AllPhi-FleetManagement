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

            builder.Services
              .AddBlazorise(options =>
              {
                  options.ChangeTextOnKeyPress = true;
              })
              .AddBootstrapProviders()
              .AddFontAwesomeIcons()
              .AddBlazoredLocalStorage()
              .AddScoped<IApiRequestService, ApiRequestService>();

            builder.RootComponents.Add<App>("#app");

            var httpClient = new HttpClient();
            var identitySection = builder.Configuration.GetSection("IdentityServer4");
            var discovery = await httpClient.GetDiscoveryDocumentAsync(identitySection.GetSection("Endpoint").Value);

            if (discovery.IsError)
                throw new Exception(discovery.Error);

            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discovery.TokenEndpoint,
                ClientId = identitySection.GetSection("ClientId").Value,
                ClientSecret = identitySection.GetSection("ClientSecret").Value,
                Scope = identitySection.GetSection("Scope").Value
            });

            if (tokenResponse.IsError)
                throw new Exception(tokenResponse.ErrorDescription);

            httpClient = new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            };
            httpClient.SetBearerToken(tokenResponse.AccessToken);

            builder.Services.AddScoped(serviceProvider => httpClient);

            var host = builder.Build();

            await host.RunAsync();
        }
    }
}
