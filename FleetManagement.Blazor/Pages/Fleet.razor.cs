using Blazored.LocalStorage;
using Blazorise;
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
        public int PageSize { get; set; } = 10;

        private List<MotorVehicleResponse> MotorVehicles { get; set; } = new List<MotorVehicleResponse>();
        private int MotorVehiclesTotal { get; set; }
        private SnackbarStack SnackbarStack { get; set; }
        private MotorVehicleFilter MotorVehicleFilter { get; set; } = new MotorVehicleFilter();
        private bool DataLoading { get; set; } = true;
        private bool FilterIsVisible { get; set; } = false;
        private DataGridColumnInfo ColumnSorted { get; set; }
        private SortDirection SortDirection { get; set; } = SortDirection.None;

        private const string _fleetStateLocalStorageKey = "fleetStateKey";

        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private IApiRequestService ApiRequestService { get; set; }
        [Inject]
        private ILocalStorageService LocalStorage { get; set; }

        private async Task ReadData(DataGridReadDataEventArgs<MotorVehicleResponse> eventArgs)
        {
            try
            {
                DataLoading = true;

                Page = eventArgs.Page;
                PageSize = eventArgs.PageSize;
                ColumnSorted = eventArgs.Columns.SingleOrDefault(x => x.Direction != SortDirection.None);

                await GetMotorVehicles();

                DataLoading = false;

                StateHasChanged();
            }
            catch (HttpRequestException)
            {
                await SnackbarStack.PushAsync("Something went wrong with the HTTP request, if this persists, contact the system administrator.", SnackbarColor.Warning);
            }
        }

        private async Task GetMotorVehicles()
        {
            var state = await LocalStorage.GetItemAsync<FleetState>(_fleetStateLocalStorageKey);

            bool descending = SortDirection switch
            {
                SortDirection.Descending => true,
                SortDirection.Ascending => false,
                SortDirection.None => false,
                _ => false,
            };

            var query = new MotorVehiclesQuery
            {
                Page = Page,
                PageSize = PageSize,
                MotorVehicleFilter = MotorVehicleFilter,
                Descending = descending,
            };

            if (ColumnSorted is not null)
                query.PropertyName = ColumnSorted.Field;

            if (state is not null)
            {
                descending = state.SortDirection switch
                {
                    SortDirection.Descending => true,
                    SortDirection.Ascending => false,
                    SortDirection.None => false,
                    _ => false,
                };

                query.MotorVehicleFilter = state.Filter;
                query.Descending = descending;
                query.Page = state.Page;
                query.PageSize = state.PageSize;

                if (state.ColumnSorted is not null)
                    query.PropertyName = state.ColumnSorted.Field;

                await LocalStorage.RemoveItemAsync(_fleetStateLocalStorageKey);
            }

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

            await GetMotorVehicles();
        }

        private async Task RowClicked(DataGridRowMouseEventArgs<MotorVehicleResponse> e)
        {
            await LocalStorage.SetItemAsync(_fleetStateLocalStorageKey, new FleetState
            {
                Filter = MotorVehicleFilter,
                Page = Page,
                PageSize = PageSize,
                FilterIsVisible = FilterIsVisible,
                SortDirection = SortDirection,
                ColumnSorted = ColumnSorted
            });

            NavigationManager.NavigateTo($"/fleet/details/{e.Item.ChassisNumber}");
        }
    }
}
