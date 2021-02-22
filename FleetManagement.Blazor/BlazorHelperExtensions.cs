using Blazorise.Snackbar;
using FleetManagement.Blazor.Responses;
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
    }
}
