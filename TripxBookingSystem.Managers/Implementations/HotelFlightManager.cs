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
    public class HotelFlightManager : ISearchable
    {
        private readonly IHotelAndFlightService _hotelAndFlightService;

        public HotelFlightManager(IHotelAndFlightService hotelAndFlightService)
        {
            _hotelAndFlightService = hotelAndFlightService;
        }

        public async Task<SearchRes> SearchAsync(SearchReq request)
        {
            var hotelAndFlightTask = await _hotelAndFlightService.SearchHotelsAndFlightsAsync(request.Destination, request.DepartureAirport);
            return HotelManagerHelper.ValidSearchResult(hotelAndFlightTask);
        }
    }
}
