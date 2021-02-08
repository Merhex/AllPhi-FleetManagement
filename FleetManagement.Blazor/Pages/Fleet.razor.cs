using Blazorise.DataGrid;
using Blazorise.Snackbar;
using FleetManagement.Blazor.Filters;
using FleetManagement.Blazor.Models;
using FleetManagement.Blazor.Queries;
using FleetManagement.Blazor.Responses;
using FleetManagement.Blazor.Services;
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
    public partial class Fleet : ComponentBase
    {
        private List<MotorVehicleResponse> MotorVehicles { get; set; } = new List<MotorVehicleResponse>();
        private int MotorVehiclesTotal { get; set; }
        private int Page { get; set; } = 1;
        private int PageSize { get; set; } = 20;
        private SnackbarStack SnackbarStack { get; set; }
        private MotorVehicleFilter MotorVehicleFilter { get; set; } = new MotorVehicleFilter();
        private bool DataLoading { get; set; } = true;
        private bool IsFilterVisible { get; set; } = false;

        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private IApiRequestService ApiRequestService { get; set; }

        private async Task ReadData(DataGridReadDataEventArgs<MotorVehicleResponse> e)
        {
            try
            {
                DataLoading = true;

                Page = e.Page;
                PageSize = e.PageSize;

                await GetOperationalVehicles(Page, PageSize);

                DataLoading = false;

                StateHasChanged();
            }
            catch (HttpRequestException)
            {
                await SnackbarStack.PushAsync("Something went wrong with the HTTP request, if this persists, contact the system administrator.", SnackbarColor.Warning);
            }
            catch (Exception)
            {
                await SnackbarStack.PushAsync("Something went wrong, contact the system administrator.", SnackbarColor.Danger);
            }
        }

        private async Task GetOperationalVehicles(int page, int pageSize)
        {
            var query = new MotorVehicleOperationalQuery(page, pageSize, MotorVehicleFilter);

            var content = await ApiRequestService.SendGetRequest<PaginatedResponse<MotorVehicleResponse>>(query);

            MotorVehicles = content.Items.ToList();
            MotorVehiclesTotal = content.TotalCount;
        }

        private void ClearFilter()
        {
            MotorVehicleFilter = new MotorVehicleFilter();
        }

        private async Task ApplyFilter()
        {
            Page = 1;

            await GetOperationalVehicles(Page, PageSize);
        }

        private void RowClicked(DataGridRowMouseEventArgs<MotorVehicleResponse> e)
        {
            NavigationManager.NavigateTo($"/fleet/details/{e.Item.ChassisNumber}");
        }
    }
}
