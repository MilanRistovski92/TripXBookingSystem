using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace TripXBookingSystem.Exceptions
{
    public class ErrorResult : IHttpActionResult
    {
        public HttpRequestMessage Request { get; set; }
        public string Content { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage(StatusCode)
            {
                Content = new StringContent(Content),
                RequestMessage = Request
            };
            return Task.FromResult(response);
        }
    }
}
