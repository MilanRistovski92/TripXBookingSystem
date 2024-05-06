using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripXBookingSystem.Models.Book;
using TripXBookingSystem.Models.Search;

namespace TripXBookingSystem.Services.Interfaces
{
    public interface IHotelService
    {
        Task<SearchRes> SearchHotelsAsync(string destinationCode);
    }
}
