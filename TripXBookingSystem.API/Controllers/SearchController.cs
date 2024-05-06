using Microsoft.AspNetCore.Mvc;
using TripxBookingSystem.Factories.Implementations;
using TripxBookingSystem.Factories.Interfaces;
using TripXBookingSystem.Models.Search;

namespace TripXBookingSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IManagerFactory _searchManagerFactory;

        public SearchController(IManagerFactory searchManagerFactory)
        {
            _searchManagerFactory = searchManagerFactory;
        }

        [HttpGet("search")]
        public async Task<SearchRes> Search([FromQuery] SearchReq request)
        {
            var searchManager = _searchManagerFactory.GetManager(request);
            if (searchManager == null)
            {
                throw new Exception();
            }
            var result = await searchManager.SearchAsync(request);
            return result;
        }
    }
}
