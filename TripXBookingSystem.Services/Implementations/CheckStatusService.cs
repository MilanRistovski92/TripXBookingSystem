using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripXBookingSystem.DataStore.Implementations;
using TripXBookingSystem.DataStore.Interfaces;
using TripXBookingSystem.Models.Book;
using TripXBookingSystem.Models.CheckStatus;
using TripXBookingSystem.Services.Interfaces;

namespace TripXBookingSystem.Services.Implementations
{
    public class CheckStatusService : ICheckStatusService
    {
        private readonly IDataStore<BookRes> _bookingStore;

        public CheckStatusService(IDataStore<BookRes> bookingStore)
        {
            _bookingStore = DataStores.BookingResults;
        }

        public async Task<CheckStatusRes> CheckStatusAsync(CheckStatusReq request)
        {
            var booking = _bookingStore.GetSingleResult(request.BookingCode);
            if (booking == null)
            {
                throw new KeyNotFoundException("No booking found with the provided code.");
            }

            var statusResponse = new CheckStatusRes
            {
                Status = booking.Status
            };

            return await Task.FromResult(statusResponse);
        }
    }
}
