using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TripXBookingSystem.DataStore.Implementations;
using TripXBookingSystem.DataStore.Interfaces;
using TripXBookingSystem.Models.Options;
using TripXBookingSystem.Models.Search;
using TripXBookingSystem.Services.Helpers;
using TripXBookingSystem.Services.Interfaces;

namespace TripXBookingSystem.Services.Implementations
{
    public class HotelAndFlightService : IHotelAndFlightService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpRequestService _httpRequest;
        private readonly string _baseUrl;
        private IDataStore<List<Option>> _optionsStore;

        public HotelAndFlightService(HttpClient httpClient, IHttpRequestService httpRequest, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpRequest = httpRequest;
            _baseUrl = "https://tripx-test-functions.azurewebsites.net/api/";
            _optionsStore = DataStores.HotelOptionResults;
        }

        public async Task<SearchRes> SearchHotelsAndFlightsAsync(string destinationCode, string departureAirport)
        {
            var cacheKey = $"hotels-{destinationCode}-{departureAirport}";
            var hotelUrl = $"{_baseUrl}SearchHotels?destinationCode={destinationCode}";
            var flightUrl = $"{_baseUrl}SearchFlights?departureAirport={departureAirport}&arrivalAirport={destinationCode}";

            var hotelRequest = _httpRequest.CreateHttpRequest(hotelUrl);
            var flightRequest = _httpRequest.CreateHttpRequest(flightUrl);

            var hotelTask = await _httpClient.SendAsync(hotelRequest);
            var flightTask = await _httpClient.SendAsync(flightRequest);

            if (!hotelTask.IsSuccessStatusCode || !flightTask.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Error retrieving data");
            }

            var hotelContentResponse = await DeserializationHelper.DeserializeResponse<List<Option>>(hotelTask);
            var flightContentResponse = await DeserializationHelper.DeserializeResponse<List<Option>>(flightTask);

            var hotelFlightResponse = new List<Option>();
            hotelFlightResponse.AddRange(hotelContentResponse ?? new List<Option>());
            hotelFlightResponse.AddRange(flightContentResponse ?? new List<Option>());
            BookHelpers.SetOptionCode(hotelFlightResponse);
            _optionsStore.StoreSingleResult(cacheKey, hotelFlightResponse);
            return new SearchRes { Options = hotelFlightResponse };
        }
    }
}
