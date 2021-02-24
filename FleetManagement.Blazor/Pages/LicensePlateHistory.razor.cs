using Blazored.LocalStorage;
using Blazorise.DataGrid;
using FleetManagement.Blazor.Filters;
using FleetManagement.Blazor.Queries;
using FleetManagement.Blazor.Responses;
using FleetManagement.Blazor.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManagement.Blazor.Pages
{
    public partial class LicensePlateHistory : ComponentBase
    {
        [Parameter]
        public string Identifier { get; set; }

        [Inject]
        private IApiRequestService ApiRequestService { get; set; }
        [Inject]
        private ILocalStorageService LocalStorage { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private List<LicensePlateSnapshotResponse> History { get; set; }
        private LicensePlateHistoryFilter LicensePlateHistoryFilter { get; set; } = new LicensePlateHistoryFilter();
        private List<DataGridColumnInfo> Columns { get; set; }
        private int TotalItems { get; set; }
        private int Page { get; set; } = 1;
        private int PageSize { get; set; } = 10;
        private bool FilterIsVisible { get; set; }
        private bool DataLoading { get; set; }

        private async Task ApplyFilter()
        {
            Page = 1;

            await SetLicensePlateHistory();
        }

        private async Task ClearFilter()
        {
            Page = 1;

            LicensePlateHistoryFilter = new LicensePlateHistoryFilter();
            LicensePlateHistoryFilter.LicensePlateFilter.Identifier = Identifier;

            await SetLicensePlateHistory();
        }

        private async Task ReadData(DataGridReadDataEventArgs<LicensePlateSnapshotResponse> eventArgs)
        {
            Page = eventArgs.Page;
            PageSize = eventArgs.PageSize;
            Columns = eventArgs.Columns.ToList();
            LicensePlateHistoryFilter.LicensePlateFilter.Identifier = Identifier;

            await SetLicensePlateHistory();
        }

        private async Task SetLicensePlateHistory()
        {
            var details = await GetLicensePlateDetails();

            TotalItems = details.TotalCount;
            History = details.Items.ToList();
        }

        private async Task<PaginatedResponse<LicensePlateSnapshotResponse>> GetLicensePlateDetails()
        {
            var query = new LicensePlateHistoryQuery
            {
                LicensePlateHistoryFilter = LicensePlateHistoryFilter,
                Page = Page,
                PageSize = PageSize,
                Sortables = Columns.GetSortables().ToList()
            };

            return await ApiRequestService.SendQuery<PaginatedResponse<LicensePlateSnapshotResponse>>(query);
        }

        private async Task Return()
        {
            var path = await LocalStorage.GetItemAsStringAsync("return");

            if (path is not null)
                NavigationManager.NavigateTo(path);

            await LocalStorage.RemoveItemAsync("return");
        }
    }
}