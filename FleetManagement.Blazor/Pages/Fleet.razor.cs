using Blazored.LocalStorage;
using Blazorise.DataGrid;
using Blazorise.Snackbar;
using FleetManagement.Blazor.Filters;
using FleetManagement.Blazor.Models;
using FleetManagement.Blazor.Queries;
using FleetManagement.Blazor.Responses;
using FleetManagement.Blazor.Services;
using FleetManagement.Blazor.States;
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
        [Parameter]
        public int Page { get; set; } = 1;
        [Parameter]
        public int PageSize { get; set; } = 15;

        private List<MotorVehicleResponse> MotorVehicles { get; set; } = new List<MotorVehicleResponse>();
        private int MotorVehiclesTotal { get; set; }
        private SnackbarStack SnackbarStack { get; set; }
        private MotorVehicleFilter MotorVehicleFilter { get; set; } = new MotorVehicleFilter();
        private bool DataLoading { get; set; } = true;
        private bool FilterIsVisible { get; set; } = false;

        private const string _fleetStateLocalStorageKey = "fleetStateKey";

        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private IApiRequestService ApiRequestService { get; set; }
        [Inject]
        private ILocalStorageService LocalStorage { get; set; }

        private async Task ReadData(DataGridReadDataEventArgs<MotorVehicleResponse> e)
        {
            try
            {
                DataLoading = true;

                Page = e.Page;
                PageSize = e.PageSize;

                var state = await LocalStorage.GetItemAsync<FleetState>(_fleetStateLocalStorageKey);

                if (state is not null)
                {
                    Page = state.Page;
                    PageSize = state.PageSize;
                    MotorVehicleFilter = state.Filter;
                    FilterIsVisible = state.FilterIsVisible;

                    await GetOperationalVehicles(state.Page, state.PageSize, state.Filter);

                    await LocalStorage.RemoveItemAsync(_fleetStateLocalStorageKey);
                }
                else
                {
                    await GetOperationalVehicles(e.Page, e.PageSize, MotorVehicleFilter);
                }

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

        private async Task GetOperationalVehicles(int page, int pageSize, MotorVehicleFilter filter)
        {
            var query = new MotorVehiclesQuery(page, pageSize, filter);

            var content = await ApiRequestService.SendGetRequest<PaginatedResponse<MotorVehicleResponse>>(query);

            MotorVehicles = content.Items.ToList();
            MotorVehiclesTotal = content.TotalCount;
        }

        private void FilterVisibilityToggle()
        {
            FilterIsVisible = !FilterIsVisible;
        }

        private async Task ClearFilter()
        {
            MotorVehicleFilter = new MotorVehicleFilter();

            await ApplyFilter();
        }

        private async Task ApplyFilter()
        {
            Page = 1;

            await GetOperationalVehicles(Page, PageSize, MotorVehicleFilter);
        }

        private async Task RowClicked(DataGridRowMouseEventArgs<MotorVehicleResponse> e)
        {
            await LocalStorage.SetItemAsync(_fleetStateLocalStorageKey, new FleetState
            {
                Filter = MotorVehicleFilter,
                Page = Page,
                PageSize = PageSize,
                FilterIsVisible = FilterIsVisible
            });

            NavigationManager.NavigateTo($"/fleet/details/{e.Item.ChassisNumber}");
        }
    }
}
