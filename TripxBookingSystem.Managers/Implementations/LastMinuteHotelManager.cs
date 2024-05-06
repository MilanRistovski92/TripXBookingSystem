using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripxBookingSystem.Managers.Helpers;
using TripxBookingSystem.Managers.Interfaces;
using TripXBookingSystem.Models.Search;
using TripXBookingSystem.Services.Interfaces;

namespace TripxBookingSystem.Managers.Implementations
{
    public class LastMinuteHotelManager : ISearchable
    {
        private readonly IHotelService _hotelService;

        public LastMinuteHotelManager(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public async Task<SearchRes> SearchAsync(SearchReq request)
        {
            HotelManagerHelper.ValidateSearchRequest(request);
            var searchResultsList = await _hotelService.SearchHotelsAsync(request.Destination);
            return HotelManagerHelper.ValidSearchResult(searchResultsList);
        }
    }
}
