using Blazorise.Charts;
using Blazorise.Snackbar;
using FleetManagement.Blazor.Commands;
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
        private bool IsLoading { get; set; } = true;
        private bool Disabled { get; set; } = true;
        private bool AlreadyInitialized { get; set; }
        private bool UpdateButtonShown { get; set; } = false;


        protected override async Task OnInitializedAsync()
        {
            var query = new MotorVehicleDetailedQuery(ChassisNumber);

            MotorVehicleDetailed = await ApiRequestService.SendGetRequest<MotorVehicleDetailedResponse>(query);

            IsLoading = false;
        }

        private void ToggleEditMode()
        {
            Disabled = !Disabled;
            UpdateButtonShown = !Disabled;
        }

        private async Task Submit()
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

            var response = await ApiRequestService.SendPutRequest(updateCommand.Endpoint, updateCommand);

            if (response.Errors.Any())
            {
                foreach (var subject in response.Errors)
                    foreach (var message in subject.Value)
                        await SnackbarStack.PushAsync(message, SnackbarColor.Danger);
            }
            else
            {
                await SnackbarStack.PushAsync("Update successful.", SnackbarColor.Success);
            }
        }

        private void ReturnToFleet()
        {
            NavigationManager.NavigateTo("/fleet");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (AlreadyInitialized is false && IsLoading is false)
            {
                AlreadyInitialized = true;

                await HandleRedraw(MileageChart, GetLineChartDataset);
            }
        }

        async Task HandleRedraw<TDataSet, TItem, TOptions, TModel>(BaseChart<TDataSet, TItem, TOptions, TModel> chart, Func<TDataSet> getDataSet)
            where TDataSet : ChartDataset<TItem>
            where TOptions : ChartOptions
            where TModel : ChartModel
        {
            await chart.Clear();

            await chart.AddLabelsDatasetsAndUpdate(GetLabels().ToList(), getDataSet());
        }

        LineChartDataset<double> GetLineChartDataset()
        {
            return new LineChartDataset<double>
            {
                Label = "Mileage snapshot dates",
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
