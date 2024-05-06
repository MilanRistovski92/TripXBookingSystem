using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Text;
using TripXBookingSystem.Services.Implementations;

namespace TripXBookingSystem.Tests
{
    public class FlightServiceTests
    {
        [Fact]
        public async Task SearchFlightsAsync_SuccessfulResponse_ReturnsSearchRes()
        {
            // Arrange
            var departureAirport = "DepartureAirport";
            var arrivalAirport = "ArrivalAirport";

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{'Property1': 'value1', 'Property2': 'value2'}")
                });

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://tripx-test-functions.azurewebsites.net")
            };
            var flightService = new FlightService(httpClient);

            // Act
            var result = await flightService.SearchFlightsAsync(departureAirport, arrivalAirport);

            // Assert
            Assert.NotNull(result);
        }
    }
}
