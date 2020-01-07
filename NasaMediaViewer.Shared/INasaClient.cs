using System.Net.Http;
using System.Threading.Tasks;
using NasaMediaViewer.Shared.Builders;
using NasaMediaViewer.Shared.Models;

namespace NasaMediaViewer.Shared
{
    public interface INasaClient
    {
        Task<NasaCollection<NasaMediaEntry>> RequestMediaAsync(string href);

        Task<NasaCollection<NasaAssetEntry>> RequestAssetsAsync(string nasaId);

        SearchRequestBuilder BuildSearchRequest();

        Task<T> ExecuteRequestAsync<T>(HttpRequestMessage requestMessage);
    }
}