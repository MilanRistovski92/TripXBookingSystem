using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripXBookingSystem.DataStore.Implementations;
using TripXBookingSystem.DataStore.Interfaces;
using TripXBookingSystem.Models.Options;
using TripXBookingSystem.Models.Search;
using TripXBookingSystem.Services.Helpers;
using TripXBookingSystem.Services.Interfaces;

namespace TripXBookingSystem.Services.Implementations
{
    public class HotelService : IHotelService
    {
        private readonly HttpClient _httpClient;
        private IDataStore<List<Option>> _optionsStore;
        private readonly string _baseUrl;

        public HotelService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _optionsStore = DataStores.HotelOptionResults;
            _baseUrl = "https://tripx-test-functions.azurewebsites.net/api/";
        }

        public async Task<SearchRes> SearchHotelsAsync(string destinationCode)
        {
            var cacheKey = $"hotels-{destinationCode}";

            var response = _httpClient.GetAsync($"{_baseUrl}SearchHotels?destinationCode={destinationCode}");
            await Task.WhenAll(response);
            var content = await response.Result.Content.ReadAsStringAsync();
            var hotelSearchResponse = JsonConvert.DeserializeObject<List<Option>>(content) ?? new List<Option>();

            BookHelpers.SetOptionCode(hotelSearchResponse);

            _optionsStore.StoreSingleResult(cacheKey, hotelSearchResponse);
            var hotelResponse = new SearchRes { Options = hotelSearchResponse };
            return await Task.FromResult(hotelResponse);
        }
    }
}
