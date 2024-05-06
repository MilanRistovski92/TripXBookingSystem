using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripXBookingSystem.Models.Options;
using TripXBookingSystem.Models.Search;

namespace TripxBookingSystem.Managers.Helpers
{
    public class HotelManagerHelper
    {
        public static void ValidateSearchRequest(SearchReq request)
        {
            if (string.IsNullOrEmpty(request.Destination))
            {
                throw new ArgumentException("Destination is required.", nameof(request));
            }

            if (request.FromDate >= request.ToDate)
            {
                throw new ArgumentException("FromDate must be earlier than ToDate.", nameof(request));
            }
        }

        public static SearchRes ValidSearchResult(SearchRes searchResult)
        {
            if (searchResult == null)
            {
                return new SearchRes { Options = new List<Option>() };
            }

            var allOptions = searchResult.Options
                .Where(sr => sr != null)
                .Select(option => new Option
                {
                    ArrivalAirport = option.ArrivalAirport,
                    OptionCode = option.OptionCode,
                    HotelCode = option.HotelCode,
                    FlightCode = option.FlightCode,
                    Price = option.Price
                }).ToList();

            return new SearchRes { Options = allOptions };
        }
    }
}
