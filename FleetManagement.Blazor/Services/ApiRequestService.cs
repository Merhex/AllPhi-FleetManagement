using FleetManagement.Blazor.Commands;
using FleetManagement.Blazor.Queries;
using FleetManagement.Blazor.Responses;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.Blazor.Services
{
    public class ApiRequestService : IApiRequestService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly string _readUrl;
        private readonly string _writeUrl;

        public ApiRequestService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;

            _readUrl =  _configuration
                            .GetSection("ApiUrls")
                            .GetValue<string>("ReadSSL")
                            ??
                            _configuration
                            .GetSection("ApiUrls")
                            .GetValue<string>("Read");

            _writeUrl = _configuration
                            .GetSection("ApiUrls")
                            .GetValue<string>("WriteSSL")
                            ??
                            _configuration
                            .GetSection("ApiUrls")
                            .GetValue<string>("Write");
        }

        public async Task<T> SendQuery<T>(IQuery query)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{_readUrl}/{query.Endpoint}"),
                Method = HttpMethod.Get
            };

            return await SendRequest<T>(request);
        }

        public async Task<IApiCommandResponse> SendCommand(IApiCommand command)
        {
            var uri = new Uri($"{_writeUrl}/{command.Endpoint}");
            var json = JsonConvert.SerializeObject(command);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = command.HttpMethod.Method switch
            {
                "POST"  => await _httpClient.PostAsync(uri, content),
                "PUT"   => await _httpClient.PutAsync(uri, content),
                _       => throw new InvalidProgramException($"The method set in the command is invalid. Method set was: {command.HttpMethod.Method}")
            };

            return await ApiCommandResponse(response);
        }

        #region PRIVATE
        private async Task<T> SendRequest<T>(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>();
        }

        private static async Task<IApiCommandResponse> ApiCommandResponse(HttpResponseMessage result)
        {
            if (result.IsSuccessStatusCode)
            {
                return new ApiCommandResponse();
            }
            else
            {
                var content = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<ApiCommandResponse>(content);

                return response;
            }
        }
        #endregion
    }
}
