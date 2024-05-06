using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripXBookingSystem.Models.Search;

namespace TripXBookingSystem.Models.Book
{
    public class BookReq
    {
        public string OptionCode { get; set; }
        public SearchReq SearchReq { get; set; }

    }
}
