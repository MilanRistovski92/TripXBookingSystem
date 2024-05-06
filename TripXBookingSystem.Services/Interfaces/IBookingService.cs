using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripXBookingSystem.Models.Book;

namespace TripXBookingSystem.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookRes> BookAsync(BookReq request);
    }
}
