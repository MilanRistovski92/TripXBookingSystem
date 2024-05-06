using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TripXBookingSystem.Models.Book;
using TripXBookingSystem.Services.Interfaces;

namespace TripXBookingSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("book")]
        public async Task<BookRes> Book([FromQuery] [Required] BookReq request)
        {
            return await _bookingService.BookAsync(request);
        }

    }
}
