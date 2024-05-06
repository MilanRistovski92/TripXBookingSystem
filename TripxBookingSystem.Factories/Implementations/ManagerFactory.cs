using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripxBookingSystem.Factories.Interfaces;
using TripxBookingSystem.Managers.Implementations;
using TripxBookingSystem.Managers.Interfaces;
using TripXBookingSystem.Models.Search;
using TripXBookingSystem.Services.Interfaces;

namespace TripxBookingSystem.Factories.Implementations
{
    public class ManagerFactory : IManagerFactory
    {
        private readonly IHotelService _hotelService;
        private readonly IFlightService _flightService;
        private readonly IHotelAndFlightService _hotelAndFlightService;

        public ManagerFactory(IHotelService hotelService, IFlightService flightService, IHotelAndFlightService hotelAndFlightService)
        {
            _hotelService = hotelService;
            _flightService = flightService;
            _hotelAndFlightService = hotelAndFlightService;
        }

        public ISearchable GetManager(SearchReq request)
        {
            if (request.DepartureAirport == null && DateTime.Now.AddDays(45) > request.FromDate)
            {
                return new LastMinuteHotelManager(_hotelService);
            }
            if (string.IsNullOrEmpty(request.DepartureAirport))
            {
                return new HotelManager(_hotelService);
            }
            else
            {
                return new HotelFlightManager(_hotelAndFlightService);
            }
        }
    }
}
