using Blazorise;
using Blazorise.DataGrid;
using Blazorise.Snackbar;
using FleetManagement.Blazor.Filters;
using FleetManagement.Blazor.Queries;
using FleetManagement.Blazor.Responses;
using FleetManagement.Blazor.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FleetManagement.Blazor.Pages
{
    public partial class LicensePlates
    {
        [Inject]
        private IApiRequestService ApiRequestService { get; set; }

        private LicensePlateFilter LicensePlateFilter { get; set; } = new LicensePlateFilter();
        private SnackbarStack SnackbarStack { get; set; }
        private bool FilterIsVisible { get; set; }
        private bool DataLoading { get; set; }
        private int Page { get; set; } = 1;
        private int PageSize { get; set; } = 10;
        private int LicensePlatesTotal { get; set; }
        private List<LicensePlateResponse> LicensePlatesItems { get; set; } = new List<LicensePlateResponse>();
        private SortDirection IdentifierSortDirection { get; set; }
        private List<DataGridColumnInfo> Columns { get; set; }

        private async Task ReadData(DataGridReadDataEventArgs<LicensePlateResponse> eventArgs)
        {
            try
            {
                DataLoading = true;

                Page = eventArgs.Page;
                PageSize = eventArgs.PageSize;
                Columns = eventArgs.Columns.ToList();

                await GetLicensePlates();

                DataLoading = false;
            }
            catch (HttpRequestException)
            {
                await SnackbarStack.PushAsync("Something went wrong fetching the data. If this persists, contact the system administrator.", SnackbarColor.Warning);
            }
        }

        private async Task GetLicensePlates()
        {
            var query = new LicensePlatesQuery
            {
                LicensePlateFilter = LicensePlateFilter,
                Page = Page,
                PageSize = PageSize,
                Sortables = GetSortables(Columns).ToList()
            };

            var content = await ApiRequestService.SendQuery<PaginatedResponse<LicensePlateResponse>>(query);

            LicensePlatesItems = content.Items.ToList();
            LicensePlatesTotal = content.TotalCount;
        }

        private void FilterVisibilityToggle()
        {
            FilterIsVisible = !FilterIsVisible;
        }

        private async Task ClearFilter()
        {
            LicensePlateFilter = new LicensePlateFilter();

            await ApplyFilter();
        }

        private async Task ApplyFilter()
        {
            Page = 1;

            await GetLicensePlates();
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

        private static bool IsDescending(SortDirection direction) => direction switch
        {
            SortDirection.Descending => true,
            SortDirection.Ascending => false,
            SortDirection.None => false,
            _ => false
        };
    }
}
