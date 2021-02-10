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
        private List<DataGridColumnInfo> Columns { get; set; }
        private MotorVehicleFilter MotorVehicleFilter { get; set; } = new MotorVehicleFilter();
        private SnackbarStack SnackbarStack { get; set; }
        private int MotorVehiclesTotal { get; set; }
        private bool DataLoading { get; set; } = true;
        private bool FilterIsVisible { get; set; } = false;

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
                Columns = eventArgs.Columns
                    .Where(x => x.Direction != SortDirection.None)
                    .ToList();

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

            var query = new MotorVehiclesQuery
            {
                Page = Page,
                PageSize = PageSize,
                MotorVehicleFilter = MotorVehicleFilter
            };

            if (state is not null)
            {
                var sortables = GetSortables(state.Columns);
                query.Sortables = sortables;

                await LocalStorage.RemoveItemAsync(_fleetStateLocalStorageKey);
            }
            else
            {
                query.Sortables = GetSortables(Columns);
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
                Columns = Columns
            });

            NavigationManager.NavigateTo($"/fleet/details/{e.Item.ChassisNumber}");
        }

        private static List<ISortable> GetSortables(List<DataGridColumnInfo> columns)
        {
            var sortables = new List<ISortable>();

            if (columns.Count is not 0)
            {
                foreach (var column in columns)
                {
                    bool descending = column.Direction switch
                    {
                        SortDirection.Descending => true,
                        SortDirection.Ascending => false,
                        SortDirection.None => false,
                        _ => false,
                    };

                    sortables.Add(new Sortable
                    {
                        Descending = descending,
                        PropertyName = column.Field
                    });
                }
            }

            return sortables;
        }
    }
}
