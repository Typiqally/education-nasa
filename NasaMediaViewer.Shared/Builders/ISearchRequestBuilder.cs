using System.Net.Http;
using System.Threading.Tasks;
using NasaMediaViewer.Shared.Models;

namespace NasaMediaViewer.Shared.Builders
{
    public interface ISearchRequestBuilder
    {
        ISearchRequestBuilder ByQuery(string query);

        ISearchRequestBuilder ByCenter(string center);

        ISearchRequestBuilder ByDescription(string description);

        ISearchRequestBuilder ByDescription508(string description);

        ISearchRequestBuilder ByKeywords(string[] keywords);

        ISearchRequestBuilder ByLocation(string location);

        ISearchRequestBuilder ByMediaType(MediaType mediaType);

        ISearchRequestBuilder ByNasaId(string nasaId);

        ISearchRequestBuilder ByPhotographer(string photographer);

        ISearchRequestBuilder BySecondaryCreator(string secondaryCreator);

        ISearchRequestBuilder ByTitle(string title);

        ISearchRequestBuilder ByYearRange(int start, int end);

        string BuildRequestUri();
        
        HttpRequestMessage BuildRequest();

        Task<NasaCollection<NasaMediaEntry>> ExecuteAsync();
    }
}