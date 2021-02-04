using Blazorise.DataGrid;
using Blazorise.Snackbar;
using FleetManagement.Blazor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FleetManagement.Blazor.Pages
{
    public partial class Fleet
    {
        List<MotorVehicleResponse> MotorVehicles { get; set; } = new List<MotorVehicleResponse>();
        int Page { get; set; } = 1;
        int PageSize { get; set; } = 20;
        SnackbarStack SnackbarStack { get; set; }

        [Inject]
        HttpClient HttpClient { get; set; }
        [Inject]
        IConfiguration Configuration { get; set; }


        async Task GetMotorVehicles(DataGridReadDataEventArgs<MotorVehicleResponse> e)
        {
            try
            {
                var readUrl = Configuration
                                .GetSection("ApiUrls")
                                .GetValue<string>("ReadSSL")
                                ??
                               Configuration
                                .GetSection("ApiUrls")
                                .GetValue<string>("Read");

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri($"{readUrl}/motorvehicles?Page={Page}&PageSize={PageSize}"),
                    Method = HttpMethod.Get
                };

                var response = await HttpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadFromJsonAsync<IEnumerable<MotorVehicleResponse>>();

                MotorVehicles = content.ToList();
            }
            catch (HttpRequestException ex)
            {
                SnackbarStack.Push(ex.Message, SnackbarColor.Danger);
            }

        }
    }
}
