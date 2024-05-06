using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TripXBookingSystem.DataStore.Implementations;
using TripXBookingSystem.DataStore.Interfaces;
using TripXBookingSystem.Models.Book;
using TripXBookingSystem.Models.Enums;
using TripXBookingSystem.Models.Options;
using TripXBookingSystem.Models.Search;
using TripXBookingSystem.Services.Helpers;
using TripXBookingSystem.Services.Interfaces;

namespace TripXBookingSystem.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private IDataStore<BookRes> _bookingStore;
        private IDataStore<List<Option>> _optionsStore;

        public BookingService()
        {
            _bookingStore = DataStores.BookingResults;
            _optionsStore = DataStores.HotelOptionResults;
        }

        public async Task<BookRes> BookAsync(BookReq request)
        {
            var key = string.IsNullOrEmpty(request.SearchReq.DepartureAirport) ?
              $"hotels-{request.SearchReq.Destination}" :
              $"hotels-{request.SearchReq.Destination}-{request.SearchReq.DepartureAirport}";

            var storedHotelOptions = _optionsStore.GetSingleResult(key) ?? new List<Option>();
            var chosenBookingOption = storedHotelOptions.Where(x => x.OptionCode == request.OptionCode).SingleOrDefault();

            if (chosenBookingOption == null || !BookHelpers.IsOptionAvailable(chosenBookingOption))
            {
                throw new InvalidOperationException("Option code does not match any available options.");
            }

            var bookingType = DetermineBookingType(request.SearchReq);

            var bookingResponse = new BookRes
            {
                BookingCode = chosenBookingOption.OptionCode,
                SleepTime = BookHelpers.GenerateSleepTime(),
                BookingTime = DateTime.Now,
                Status = BookingStatus.Pending,
                BookingType = bookingType
            };

            _bookingStore.StoreSingleResult(chosenBookingOption.OptionCode, bookingResponse);
            await UpdateBookingStatus(bookingResponse);

            return await Task.FromResult(bookingResponse);
        }

        private BookingType DetermineBookingType(SearchReq searchRequest)
        {
            if (searchRequest.DepartureAirport != null)
            {
                return BookingType.HotelAndFlight;
            }
            if ((searchRequest.FromDate - DateTime.Now).TotalDays <= 45)
            {
                return BookingType.LastMinuteHotels;
            }
            else
            {
                return BookingType.HotelOnly;
            }
        }

        private async Task<BookingStatus> UpdateBookingStatus(BookRes booking)
        {
            await Task.Delay(TimeSpan.FromSeconds(booking.SleepTime/5));
            if (booking.BookingType == BookingType.LastMinuteHotels)
            {
                booking.Status = BookingStatus.Failed;
            }
            else
            {
                booking.Status = BookingStatus.Success;
            }

            _bookingStore.Update(booking.BookingCode, booking, b => b.BookingCode == booking.BookingCode);
            return booking.Status;
        }

        // added for testability
        public void SetDataStores(IDataStore<BookRes> bookingStore, IDataStore<List<Option>> optionsStore)
        {
            _bookingStore = bookingStore;
            _optionsStore = optionsStore;
        }
    }
}
