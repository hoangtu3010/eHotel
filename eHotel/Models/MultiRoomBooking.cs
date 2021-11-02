using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eHotel.Models
{
    public class MultiRoomBooking
    {
        public Room room { get; set; }

        public Booking booking { get; set; }
    }
}