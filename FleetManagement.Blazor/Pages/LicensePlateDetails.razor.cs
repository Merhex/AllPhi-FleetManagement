﻿using Blazorise.DataGrid;
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
    public partial class LicensePlateDetails : ComponentBase
    {
        [Parameter]
        public string Identifier { get; set; }

        [Inject]
        private IApiRequestService ApiRequestService { get; set; }

        private List<LicensePlateSnapshotResponse> History { get; set; }
        private LicensePlateHistoryFilter LicensePlateHistoryFilter { get; set; } = new LicensePlateHistoryFilter();
        private List<DataGridColumnInfo> Columns { get; set; }
        private int TotalItems { get; set; }
        private int Page { get; set; } = 1;
        private int PageSize { get; set; } = 10;

        private async Task ReadData(DataGridReadDataEventArgs<LicensePlateSnapshotResponse> eventArgs)
        {
            Page = eventArgs.Page;
            PageSize = eventArgs.PageSize;
            Columns = eventArgs.Columns.ToList();

            var details = await GetLicensePlateDetails();

            TotalItems = details.TotalCount;
            History = details.Items.ToList();

            await GetLicensePlateDetails();
        }

        private async Task<PaginatedResponse<LicensePlateSnapshotResponse>> GetLicensePlateDetails()
        {
            var query = new LicensePlateDetailedQuery
            {
                LicensePlateHistoryFilter = LicensePlateHistoryFilter,
                Page = Page,
                PageSize = PageSize,
                Sortables = Columns.GetSortables().ToList()
            };

            return await ApiRequestService.SendQuery<PaginatedResponse<LicensePlateSnapshotResponse>>(query);
        }
    }
}
