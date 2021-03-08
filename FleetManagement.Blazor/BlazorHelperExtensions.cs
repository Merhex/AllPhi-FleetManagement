using Blazorise;
using Blazorise.DataGrid;
using Blazorise.Snackbar;
using FleetManagement.Blazor.Filters;
using FleetManagement.Blazor.Queries;
using FleetManagement.Blazor.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManagement.Blazor
{
    public static class BlazorHelperExtensions
    {
        public static async Task<IApiCommandResponse> ShowErrorsWithSnackbar(this IApiCommandResponse response, SnackbarStack snackbar)
        {
            foreach (var subject in response.Errors)
                foreach (var message in subject.Value)
                    await snackbar.PushAsync(message, SnackbarColor.Danger);

            return response;
        }

        public static string Activity(this ActivityFilterSelect filterSelect)
        {
            return filterSelect switch
            {
                ActivityFilterSelect.Active => "true",
                ActivityFilterSelect.Inactive => "false",
                ActivityFilterSelect.All => null,
                _ => null
            };
        }

        public static bool IsDescending(this SortDirection direction) => direction switch
        {
            SortDirection.Descending => true,
            SortDirection.Ascending => false,
            SortDirection.None => false,
            _ => false
        };


        public static IEnumerable<ISortable> GetSortables(this IEnumerable<DataGridColumnInfo> columns)
        {
            if (columns.Any())
                foreach (var column in columns)
                {
                    if (column.Direction is SortDirection.None)
                        continue;

                    yield return new Sortable
                    {
                        Descending = column.Direction.IsDescending(),
                        PropertyName = column.Field
                    };
                }
        }
    }
}
