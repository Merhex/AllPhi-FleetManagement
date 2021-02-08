using Blazorise.Snackbar;
using FleetManagement.Blazor.Commands;
using FleetManagement.Blazor.Queries;
using FleetManagement.Blazor.Responses;
using FleetManagement.Blazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
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
        private MotorVehicleDetailedResponse MotorVehicleDetailed { get; set; }
        private bool IsLoading { get; set; } = true;
        private bool Disabled { get; set; } = true;
        private bool UpdateButtonShown { get; set; } = false;
        private SnackbarStack SnackbarStack { get; set; }


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
                ChassisNumber  = MotorVehicleDetailed.ChassisNumber,
                BodyType       = MotorVehicleDetailed.BodyType,
                Brand          = MotorVehicleDetailed.Brand,
                Model          = MotorVehicleDetailed.Model,
                Operational    = MotorVehicleDetailed.Operational,
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
    }
}
