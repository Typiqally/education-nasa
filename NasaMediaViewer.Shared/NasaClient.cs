using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using NasaMediaViewer.Shared.Builders;
using NasaMediaViewer.Shared.Models;

namespace NasaMediaViewer.Shared
{
    public sealed class NasaClient : INasaClient
    {
        private readonly HttpClient _httpClient;

        public NasaClient(Uri baseAddress)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = baseAddress ?? throw new ArgumentNullException(nameof(baseAddress))
            };
        }

        public static NasaClient Default() => new NasaClient(new Uri(NasaConstants.ApiBaseUrl));

        public async Task<NasaCollection<NasaMediaEntry>> RequestSortedMediaAsync(OrderType order)
        {
            return await RequestMediaAsync($"asset/?orderby={order.ToString().ToLower()}");
        }

        public async Task<NasaCollection<NasaMediaEntry>> RequestMediaAsync(string href)
        {
            return await RequestMediaAsync(BuildRequestMessage(href));
        }

        public async Task<NasaCollection<NasaMediaEntry>> RequestMediaAsync(HttpRequestMessage requestMessage)
        {
            return (await ExecuteRequestAsync<NasaCollectionDto<NasaMediaEntry>>(requestMessage)).Collection;
        }

        public async Task<NasaCollection<NasaAssetEntry>> RequestAssetsAsync(string nasaId)
        {
            return (await ExecuteRequestAsync<NasaCollectionDto<NasaAssetEntry>>($"asset/{nasaId}")).Collection;
        }

        public SearchRequestBuilder BuildSearchRequest()
        {
            return new SearchRequestBuilder(this);
        }

        public static HttpRequestMessage BuildRequestMessage(string href)
        {
            return new HttpRequestMessage(HttpMethod.Get, href);
        }

        public async Task<T> ExecuteRequestAsync<T>(string href)
        {
            return await ExecuteRequestAsync<T>(BuildRequestMessage(href));
        }

        public async Task<T> ExecuteRequestAsync<T>(HttpRequestMessage requestMessage)
        {
            // Debug
            Debug.WriteLine(requestMessage.RequestUri.ToString().StartsWith("http")
                ? $"Executing request on: {requestMessage.RequestUri}"
                : $"Executing request on: {_httpClient.BaseAddress}{requestMessage.RequestUri}");

            // Send a request then store it as response
            var response = await _httpClient
                .SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);

            // Get the content stream from the response
            using var contentStream = await response.Content.ReadAsStreamAsync();

            // Deserialize the response content stream
            return await JsonSerializer.DeserializeAsync<T>(contentStream);
        }
    }
}