using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TripXBookingSystem.DataStore.Interfaces;
using TripXBookingSystem.Services.Interfaces;

namespace TripXBookingSystem.Services.Implementations
{
    public class HttpRequestService : IHttpRequestService
    {
        private readonly IDataStore<string> _tokenResult;

        public HttpRequestService(IDataStore<string> tokenResult)
        {
            _tokenResult = tokenResult;
        }

        public HttpRequestMessage CreateHttpRequest(string url)
        {
            string token = _tokenResult.GetSingleResult("token");
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return request;
        }
    }
}
