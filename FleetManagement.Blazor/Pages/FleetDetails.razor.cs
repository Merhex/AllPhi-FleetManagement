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

        protected override async Task OnInitializedAsync()
        {
            var query = new MotorVehicleDetailedQuery(ChassisNumber);

            MotorVehicleDetailed = await ApiRequestService.SendGetRequest<MotorVehicleDetailedResponse>(query);

            IsLoading = false;
        }

        public void ToggleEditMode()
        {
            Disabled = !Disabled;
        }
    }
}
