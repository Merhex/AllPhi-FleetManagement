using Blazorise.DataGrid;
using Blazorise.Snackbar;
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
    public partial class Drivers : ComponentBase
    {
        [Parameter]
        public int Page { get; set; } = 1;
        [Parameter]
        public int PageSize { get; set; } = 10;

        public List<DriverResponse> DriverItems { get; set; }
        public int DriverTotal { get; set; }
        public bool DataLoading { get; set; }
        public IEnumerable<DataGridColumnInfo> Columns { get; set; }
        public SnackbarStack SnackbarStack { get; set; }

        [Inject]
        public IApiRequestService ApiRequestService { get; set; }

        private async Task ReadData(DataGridReadDataEventArgs<DriverResponse> eventArgs)
        {
            try
            {
                DataLoading = true;

                Page = eventArgs.Page;
                PageSize = eventArgs.PageSize;
                Columns = eventArgs.Columns;

                await GetDrivers();

                DataLoading = false;
            }
            catch (HttpRequestException)
            {
                await SnackbarStack.PushAsync("Something went wrong fetching the data. If this persists, contact the system administrator.", SnackbarColor.Warning);
            }
        }

        private async Task GetDrivers()
        {
            var query = new DriversQuery
            {
                Page = Page,
                PageSize = PageSize,
                Sortables = Columns.GetSortables().ToList()
            };

            var content = await ApiRequestService.SendQuery<PaginatedResponse<DriverResponse>>(query);

            DriverItems = content.Items.ToList();
            DriverTotal = content.TotalCount;
        }
    }
}
