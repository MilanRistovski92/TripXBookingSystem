using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripXBookingSystem.Models.Enums;

namespace TripXBookingSystem.Models.Book
{
    public class BookRes
    {
        public string BookingCode { get; set; }
        public int SleepTime { get; set; }
        public DateTime BookingTime { get; set; }
        public BookingStatus Status { get; set; }
        public BookingType BookingType { get; set; }
    }
}
