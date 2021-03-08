using Blazored.LocalStorage;
using Blazorise;
using Blazorise.DataGrid;
using Blazorise.Snackbar;
using FleetManagement.Blazor.Commands;
using FleetManagement.Blazor.Filters;
using FleetManagement.Blazor.Queries;
using FleetManagement.Blazor.Responses;
using FleetManagement.Blazor.Services;
using FleetManagement.Blazor.States;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FleetManagement.Blazor.Pages
{
    public partial class LicensePlates
    {
        [Inject]
        private IApiRequestService ApiRequestService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private ILocalStorageService LocalStorage { get; set; }
        [Inject]
        private ISyncLocalStorageService SynchronousLocalStorage { get; set; }

        private LicensePlateFilter LicensePlateFilter { get; set; } = new LicensePlateFilter();
        private SnackbarStack SnackbarStack { get; set; }
        public Validations LicensePlateValidations { get; set; }
        private bool InChangeStatusOperation { get; set; }
        private bool FilterIsVisible { get; set; }
        public bool AddLicensePlateShown { get; set; }
        public bool IsAddingLicensePlate { get; set; }
        private bool DataLoading { get; set; }
        public string AddLicensePlateIdentifier { get; set; }
        private int Page { get; set; } = 1;
        private int PageSize { get; set; } = 10;
        private int LicensePlatesTotal { get; set; }
        private List<LicensePlateResponse> LicensePlatesItems { get; set; } = new List<LicensePlateResponse>();
        private SortDirection IdentifierSortDirection { get; set; }
        private List<DataGridColumnInfo> Columns { get; set; }
        private SortDirection InUseDirection { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var state = SynchronousLocalStorage.GetItem<LicensePlatesState>("state");

            if (state is not null)
            {
                Page = state.FilterState.Page;
                PageSize = state.FilterState.PageSize;
                LicensePlateFilter = state.Filter;

                foreach (var columnState in state.FilterState.ColumnStates)
                {
                    _ = columnState.Field switch
                    {
                        "Identifier" => IdentifierSortDirection = columnState.Direction,
                        "InUse" => InUseDirection = columnState.Direction,
                        _ => _ = columnState.Direction
                    };
                }
            }

            await LocalStorage.RemoveItemAsync("state");
        }

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

        private async Task AddLicensePlate()
        {
            IsAddingLicensePlate = true;

            var validation = LicensePlateValidations.ValidateAll();
            if (validation is false) return;

            var command = new CreateLicensePlateCommand
            {
                Identifier = AddLicensePlateIdentifier
            };

            var response = await ApiRequestService.SendCommand(command);

            if (response.Errors.Any())
            {
                await response.ShowErrorsWithSnackbar(SnackbarStack);
            }
            else
            {
                await GetLicensePlates();
                await SnackbarStack.PushAsync("Succesfully added new license plate.", SnackbarColor.Success);
            }

            IsAddingLicensePlate = false;
        }

        private async Task LicensePlateHistoryPage(string identifier)
        {
            var filterState = new FilterState
            {
                ColumnStates = StateHelper.GetColumnState(Columns).ToList(),
                Page = Page,
                PageSize = PageSize,
                FilterIsVisible = FilterIsVisible
            };

            await LocalStorage.SetItemAsync("state", new LicensePlatesState
            {
                Filter = LicensePlateFilter,
                FilterState = filterState
            });

            await LocalStorage.SetItemAsync("return", "/licensePlates");

            NavigationManager.NavigateTo($"/licensePlates/history/{identifier}");
        }

        private static void ValidateLicensePlate(ValidatorEventArgs e)
        {
            var identifier = Convert.ToString(e.Value);

            var match = Regex.Match(identifier, "[A-Z-0-9.]{1,9}");

            if (match.Success)
            {
                e.Status = ValidationStatus.Success;
            }
            else
            {
                e.Status = ValidationStatus.Error;
            }
        }

        private async Task ChangeLicensePlateStatus(bool status, LicensePlateResponse licensePlate)
        {
            InChangeStatusOperation = true;

            var changeOperation = status switch
            {
                true => ActivateLicensePlate(licensePlate),
                false => DeactivateLicensePlate(licensePlate)
            };

            await changeOperation;

            InChangeStatusOperation = false;
        }

        private async Task DeactivateLicensePlate(LicensePlateResponse licensePlate)
        {
            var command = new DeactivateLicensePlateCommand
            {
                Identifier = licensePlate.Identifier
            };

            var response = await ApiRequestService.SendCommand(command);

            if (response.Errors.Any())
            {
                await response.ShowErrorsWithSnackbar(SnackbarStack);
            }
            else
            {
                licensePlate.InUse = false;

                StateHasChanged();

                await SnackbarStack.PushAsync("✓", "Success", SnackbarColor.Success);
            }
        }

        private async Task ActivateLicensePlate(LicensePlateResponse licensePlate)
        {
            var command = new ActivateLicensePlateCommand
            {
                Identifier = licensePlate.Identifier
            };

            var response = await ApiRequestService.SendCommand(command);

            if (response.Errors.Any())
            {
                await response.ShowErrorsWithSnackbar(SnackbarStack);
            }
            else
            {
                licensePlate.InUse = true;

                StateHasChanged();

                await SnackbarStack.PushAsync("✓", "Success", SnackbarColor.Success);
            }
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
