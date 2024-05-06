using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripXBookingSystem.DataStore.Interfaces;
using TripXBookingSystem.Models.Book;
using TripXBookingSystem.Models.CheckStatus;
using TripXBookingSystem.Services.Implementations;

namespace TripXBookingSystem.Tests
{
    public class CheckStatusServiceTests
    {
        [Fact]
        public async Task CheckStatusAsync_BookingDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            var bookingCode = "NonExistentBooking";

            var mockBookingStore = new Mock<IDataStore<BookRes>>();
            var service = new CheckStatusService(mockBookingStore.Object);

            var request = new CheckStatusReq
            {
                BookingCode = bookingCode
            };

            mockBookingStore.Setup(x => x.GetSingleResult(bookingCode)).Returns((BookRes)null);

            // Act and Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.CheckStatusAsync(request));
        }
    }
}
