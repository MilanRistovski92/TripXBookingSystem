using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripXBookingSystem.Models.Search
{
    public class SearchReq
    {
        public string Destination { get; set; }
        public string? DepartureAirport { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

}
