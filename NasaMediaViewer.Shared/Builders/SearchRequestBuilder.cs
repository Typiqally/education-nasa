using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using NasaMediaViewer.Shared.Models;

namespace NasaMediaViewer.Shared.Builders
{
    public class SearchRequestBuilder : ISearchRequestBuilder
    {
        private readonly INasaClient _client;
        private readonly Dictionary<string, string> _parameters = new Dictionary<string, string>();

        public SearchRequestBuilder(INasaClient client)
        {
            _client = client;
        }

        public ISearchRequestBuilder ByQuery(string query)
        {
            _parameters["q"] = query;

            return this;
        }

        public ISearchRequestBuilder ByCenter(string center)
        {
            throw new NotImplementedException();
        }

        public ISearchRequestBuilder ByDescription(string description)
        {
            throw new NotImplementedException();
        }

        public ISearchRequestBuilder ByDescription508(string description)
        {
            throw new NotImplementedException();
        }

        public ISearchRequestBuilder ByKeywords(string[] keywords)
        {
            throw new NotImplementedException();
        }

        public ISearchRequestBuilder ByLocation(string location)
        {
            throw new NotImplementedException();
        }

        public ISearchRequestBuilder ByMediaType(MediaType mediaType)
        {
            throw new NotImplementedException();
        }

        public ISearchRequestBuilder ByNasaId(string nasaId)
        {
            throw new NotImplementedException();
        }

        public ISearchRequestBuilder ByPhotographer(string photographer)
        {
            throw new NotImplementedException();
        }

        public ISearchRequestBuilder BySecondaryCreator(string secondaryCreator)
        {
            throw new NotImplementedException();
        }

        public ISearchRequestBuilder ByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public ISearchRequestBuilder ByYearRange(int start, int end)
        {
            throw new NotImplementedException();
        }

        public string BuildRequestUri()
        {
            return QueryHelpers.AddQueryString("search", _parameters);
        }
        
        public HttpRequestMessage BuildRequest()
        {
            var requestUri = QueryHelpers.AddQueryString("search", _parameters);
            
            return new HttpRequestMessage(HttpMethod.Get, requestUri);
        }

        public async Task<NasaCollection<NasaMediaEntry>> ExecuteAsync()
        {
            return await _client.RequestMediaAsync(BuildRequestUri());
        }
    }
}