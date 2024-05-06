using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripXBookingSystem.Models.Options;
using TripXBookingSystem.Models.Search;
using TripXBookingSystem.Services.Helpers;
using TripXBookingSystem.Services.Interfaces;

namespace TripXBookingSystem.Services.Implementations
{
    public class FlightService : IFlightService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public FlightService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = "https://tripx-test-functions.azurewebsites.net/api/";
        }

        public async Task<SearchRes> SearchFlightsAsync(string departureAirport, string arrivalAirport)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}SearchFlights?departureAirport={departureAirport}&arrivalAirport={arrivalAirport}");
            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Error retrieving data");
            }

            var flightResponse = await response.Content.ReadAsStringAsync();
            var flightSearchResponse = JsonConvert.DeserializeObject<SearchRes>(flightResponse);
            return flightSearchResponse ?? new SearchRes();
        }
    }
}
