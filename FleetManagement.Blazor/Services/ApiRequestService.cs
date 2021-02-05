using FleetManagement.Blazor.Queries;
using FleetManagement.Blazor.Responses;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FleetManagement.Blazor.Services
{
    public class ApiRequestService : IApiRequestService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly string _readUrl;

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
        }

        public async Task<T> SendGetRequest<T>(IQuery query)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{_readUrl}/{query.Endpoint}"),
                Method = HttpMethod.Get
            };

            return await SendRequest<T>(request);
        }

        #region PRIVATE
        private async Task<T> SendRequest<T>(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>();
        }
        #endregion
    }
}
