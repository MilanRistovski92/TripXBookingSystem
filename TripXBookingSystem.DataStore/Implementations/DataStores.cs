using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripXBookingSystem.DataStore.BaseDataStore;
using TripXBookingSystem.DataStore.Interfaces;
using TripXBookingSystem.Models.Book;
using TripXBookingSystem.Models.Options;
using TripXBookingSystem.Models.Search;

namespace TripXBookingSystem.DataStore.Implementations
{
    public static class DataStores
    {
        public static IDataStore<BookRes> BookingResults { get; private set; }
        public static IDataStore<SearchRes> HotelSearchResults { get; private set; }
        public static IDataStore<List<Option>> HotelOptionResults { get; private set; }
        public static IDataStore<string> TokenResults { get; private set; }

        static DataStores()
        {
            BookingResults = new BaseDataStore<BookRes>();
            HotelSearchResults = new BaseDataStore<SearchRes>();
            HotelOptionResults = new BaseDataStore<List<Option>>();
            TokenResults = new BaseDataStore<string>();
        }
    }
}
