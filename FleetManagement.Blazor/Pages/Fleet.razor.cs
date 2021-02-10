﻿using Blazored.LocalStorage;
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
using Newtonsoft.Json;
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
        private SortDirection ChassisNumberSortDirection { get; set; }
        private SortDirection ModelSortDirection { get; set; }
        private SortDirection BrandSortDirection { get; set; }
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
            DataLoading = true;

            var state = await LocalStorage.GetItemAsync<FleetState>(_fleetStateLocalStorageKey);

            if (state is not null)
            {
                Page = state.Page;
                PageSize = state.PageSize;

                foreach (var columnState in state.ColumnStates)
                {
                    var x = columnState.Field switch
                    {
                        "Brand" => BrandSortDirection = columnState.Direction,
                        "Model" => ModelSortDirection = columnState.Direction,
                        "ChassisNumber" => ChassisNumberSortDirection = columnState.Direction,
                        _ => _ = columnState.Direction
                    };
                }
            }
            else
            {
                Page = eventArgs.Page;
                PageSize = eventArgs.PageSize;
                Columns = eventArgs.Columns.ToList();
            }

            await GetMotorVehicles();

            DataLoading = false;

            StateHasChanged();
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
                query.Sortables = GetSortables(state.ColumnStates).ToList();

                await LocalStorage.RemoveItemAsync(_fleetStateLocalStorageKey);
            }
            else
            {
                query.Sortables = GetSortables(Columns).ToList();
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
                ColumnStates = GetColumnState(Columns).ToList()
            });

            NavigationManager.NavigateTo($"/fleet/details/{e.Item.ChassisNumber}");
        }

        private static IEnumerable<ColumnState> GetColumnState(List<DataGridColumnInfo> columns)
        {
            foreach (var column in columns)
            {
                yield return new ColumnState
                {
                    Direction = column.Direction,
                    Field = column.Field
                };
            }
        }

        private static IEnumerable<ISortable> GetSortables(List<DataGridColumnInfo> columns)
        {
            if (columns.Count is not 0)
                foreach (var column in columns)
                {
                    if (column.Direction is SortDirection.None) 
                        continue;

                    yield return new Sortable
                    {
                        Descending = IsDescending(column.Direction),
                        PropertyName = column.Field
                    };
                }
        }

        private static IEnumerable<ISortable> GetSortables(List<ColumnState> columnStates)
        {
            if (columnStates.Count is not 0)
                foreach (var state in columnStates)
                {
                    if (state.Direction is SortDirection.None)
                        continue;

                    yield return new Sortable
                    {
                        Descending = IsDescending(state.Direction),
                        PropertyName = state.Field
                    };
                }
        }

        private static bool IsDescending(SortDirection direction) => direction switch
        {
            SortDirection.Descending => true,
            SortDirection.Ascending => false,
            SortDirection.None => false,
            _ => false
        };
    }
}
