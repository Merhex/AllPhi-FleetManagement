using Blazorise.Charts;
using Blazorise.Snackbar;
using FleetManagement.Blazor.Commands;
using FleetManagement.Blazor.Models;
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
    public partial class FleetDetails : ComponentBase
    {
        [Parameter]
        public string ChassisNumber { get; set; }

        [Inject]
        private IApiRequestService ApiRequestService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private LineChart<double> MileageChart { get; set; } = new LineChart<double>();
        private MotorVehicleDetailedResponse MotorVehicleDetailed { get; set; }
        private SnackbarStack SnackbarStack { get; set; }
        public string LicensePlateToBeAssigned { get; set; }
        private bool IsLoading { get; set; } = true;
        private bool IsAddingMileage { get; set; }
        private bool Disabled { get; set; } = true;
        private bool AlreadyInitialized { get; set; } = false;
        public bool IsAssigningLicensePlate { get; set; } = false;
        private bool UpdateButtonShown { get; set; } = false;
        private bool AddLicensePlateShown { get; set; } = false;
        private bool MileageAddShown { get; set; } = false;
        private int MileageSnapshotValue { get; set; } = 0;
        public DateTime MileageSnapshotDate { get; set; } = DateTime.Now;


        protected override async Task OnInitializedAsync()
        {
            await GetDetailedMotorVehicle();

            IsLoading = false;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (AlreadyInitialized is false && IsLoading is false)
            {
                AlreadyInitialized = true;

                await HandleRedraw(MileageChart, GetLineChartDataset);
            }
        }

        private void ToggleEditMode()
        {
            Disabled = !Disabled;
            UpdateButtonShown = !Disabled;
        }

        private void ToggleMileageAddMode()
        {
            MileageAddShown = !MileageAddShown;
        }

        private void ToggleAddLicensePlateShown()
        {
            AddLicensePlateShown = !AddLicensePlateShown;
        }

        private async Task UpdateMotorVehicle()
        {
            UpdateButtonShown = false;
            Disabled = true;

            var updateCommand = new UpdateMotorVehicleCommand()
            {
                ChassisNumber = MotorVehicleDetailed.ChassisNumber,
                BodyType = MotorVehicleDetailed.BodyType,
                Brand = MotorVehicleDetailed.Brand,
                Model = MotorVehicleDetailed.Model,
                Operational = MotorVehicleDetailed.Operational,
                PropulsionType = MotorVehicleDetailed.PropulsionType
            };

            var response = await ApiRequestService.SendCommand(updateCommand);

            if (response.Errors.Any())
            {
                await ShowErrorsWithSnackbar(response);
            }
            else
            {
                await SnackbarStack.PushAsync("Update successful.", SnackbarColor.Success);
            }
        }

        private async Task AddMileageSnapshot()
        {
            IsAddingMileage = true;

            var command = new AddMileageToMotorVehicleCommand
            {
                ChassisNumber = MotorVehicleDetailed.ChassisNumber,
                Date = MileageSnapshotDate,
                Mileage = MileageSnapshotValue
            };

            var response = await ApiRequestService.SendCommand(command);

            if (response.Errors.Any())
            {
                await ShowErrorsWithSnackbar(response);
            }
            else
            {
                await GetDetailedMotorVehicle();

                await HandleRedraw(MileageChart, GetLineChartDataset);

                await SnackbarStack.PushAsync("Added mileage successfully.", SnackbarColor.Success);
            }

            IsAddingMileage = false;
        }

        private async Task AssignLicensePlate()
        {
            IsAssigningLicensePlate = true;

            var command = new AssignLicensePlateToMotorVehicleCommand
            {
                ChassisNumber = MotorVehicleDetailed.ChassisNumber,
                Identifier = LicensePlateToBeAssigned,
            };

            var response = await ApiRequestService.SendCommand(command);

            if (response.Errors.Any())
            {
                await ShowErrorsWithSnackbar(response);
            }
            else
            {
                await SnackbarStack.PushAsync("Assigned license plate successfully.", SnackbarColor.Success);
            }

            IsAssigningLicensePlate = false;
        }

        private async Task ShowErrorsWithSnackbar(IApiCommandResponse response)
        {
            foreach (var subject in response.Errors)
                foreach (var message in subject.Value)
                    await SnackbarStack.PushAsync(message, SnackbarColor.Danger);
        }

        private void ReturnToFleet()
        {
            NavigationManager.NavigateTo("/fleet");
        }

        private async Task GetDetailedMotorVehicle()
        {
            var query = new MotorVehicleDetailedQuery(ChassisNumber);

            MotorVehicleDetailed = await ApiRequestService.SendQuery<MotorVehicleDetailedResponse>(query);
        }


        private async Task HandleRedraw<TDataSet, TItem, TOptions, TModel>(BaseChart<TDataSet, TItem, TOptions, TModel> chart, Func<TDataSet> getDataSet)
            where TDataSet : ChartDataset<TItem>
            where TOptions : ChartOptions
            where TModel : ChartModel
        {
            await chart.Clear();

            await chart.AddLabelsDatasetsAndUpdate(GetLabels().ToList(), getDataSet());
        }

        private LineChartDataset<double> GetLineChartDataset()
        {
            return new LineChartDataset<double>
            {
                Label = "Mileage",
                Data = GetData().ToList(),
                BackgroundColor = ChartColor.FromRgba(10, 233, 249, 0.2f).ToJsRgba(),
                BorderColor = ChartColor.FromRgba(10, 233, 249, 1f).ToJsRgba(),
                Fill = true,
                PointRadius = 3,
                BorderWidth = 1,
                PointBorderColor = { ChartColor.FromRgba(10, 233, 249, 1f).ToJsRgba() }
            };
        }

        private IEnumerable<double> GetData()
        {
            foreach (var snapshot in MotorVehicleDetailed.MileageHistory)
            {
                yield return snapshot.Mileage;
            }
        }
        private IEnumerable<string> GetLabels()
        {
            foreach (var snapshot in MotorVehicleDetailed.MileageHistory)
            {
                yield return snapshot.SnapshotDate.ToShortDateString();
            }
        }
    }
}
