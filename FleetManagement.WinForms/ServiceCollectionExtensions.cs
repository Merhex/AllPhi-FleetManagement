using FleetManagement.WinForms.Services;
using FleetManagement.WinForms.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace FleetManagement.WinForms
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection collection)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            collection.AddTransient<IConfiguration>(_ => configuration);
            collection.AddTransient<IApiRequestService, ApiRequestService>();
            collection.AddTransient<HttpClient>();
            collection.AddTransient<MainForm>();

            return collection;
        }
    }
}
