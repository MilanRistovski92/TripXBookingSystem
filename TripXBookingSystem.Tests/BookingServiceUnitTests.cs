using Moq;
using TripXBookingSystem.DataStore.Interfaces;
using TripXBookingSystem.Models.Book;
using TripXBookingSystem.Models.Enums;
using TripXBookingSystem.Models.Options;
using TripXBookingSystem.Models.Search;
using TripXBookingSystem.Services.Implementations;

namespace TripXBookingSystem.Tests
{
    public class BookingServiceUnitTests
    {
        [Fact]
        public async Task BookAsync_ValidOption_ReturnsSuccessfulBooking()
        {
            // Arrange
            var mockBookingStore = new Mock<IDataStore<BookRes>>();
            var mockOptionsStore = new Mock<IDataStore<List<Option>>>();
            var optionCode = "Option";
            var destination = "Destination";

            var options = new List<Option>
            {
                new Option { OptionCode = optionCode, HotelCode = "Hotel", Price = 100 }
            };

            mockOptionsStore.Setup(x => x.GetSingleResult($"hotels-{destination}")).Returns(options);

            var service = new BookingService();
            service.SetDataStores(mockBookingStore.Object, mockOptionsStore.Object);
            var bookReq = new BookReq
            {
                OptionCode = optionCode,
                SearchReq = new SearchReq { Destination = destination, FromDate = DateTime.Now.AddDays(60) } 
            };

            // Act
            var result = await service.BookAsync(bookReq);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(optionCode, result.BookingCode);
            Assert.Equal(BookingStatus.Success, result.Status);
        }

        [Fact]
        public async Task BookAsync_NoOptions_ThrowsInvalidOperationException()
        {
            // Arrange
            var mockBookingStore = new Mock<IDataStore<BookRes>>();
            var mockOptionsStore = new Mock<IDataStore<List<Option>>>();
            var destination = "Destination";

            mockOptionsStore.Setup(x => x.GetSingleResult($"hotels-{destination}")).Returns(new List<Option>());

            var service = new BookingService();
            var bookReq = new BookReq
            {
                OptionCode = "InvalidOption",
                SearchReq = new SearchReq { Destination = destination }
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.BookAsync(bookReq));
        }

        [Fact]
        public async Task BookAsync_OptionNotAvailable_ThrowsInvalidOperationException()
        {
            // Arrange
            var mockBookingStore = new Mock<IDataStore<BookRes>>();
            var mockOptionsStore = new Mock<IDataStore<List<Option>>>();
            var destination = "Destination";
            var options = new List<Option> { new Option { OptionCode = "SomeOption", HotelCode = "Hotel", Price = 200 } };

            mockOptionsStore.Setup(x => x.GetSingleResult($"hotels-{destination}")).Returns(options);

            var service = new BookingService();
            var bookReq = new BookReq
            {
                OptionCode = "InvalidOption",
                SearchReq = new SearchReq { Destination = destination }
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.BookAsync(bookReq));
        }
    }
}