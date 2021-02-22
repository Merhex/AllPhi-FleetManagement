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

        private LicensePlateDetailedResponse LicensePlate { get; set; }
        private bool IsInitializing { get; set; } = true;

        protected async override Task OnInitializedAsync()
        {
            LicensePlate = await GetLicensePlate();

            IsInitializing = false;
        }

        private async Task<LicensePlateDetailedResponse> GetLicensePlate()
        {
            var query = new LicensePlateDetailedQuery(Identifier);

            return await ApiRequestService.SendQuery<LicensePlateDetailedResponse>(query);
        }
    }
}
