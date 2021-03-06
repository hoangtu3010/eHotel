using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eHotel.Models
{
    public class MultipleAdmin
    {
        public IEnumerable<Room> room { get; set; }

        public IEnumerable<User> user { get; set; }

        public IEnumerable<Booking> booking { get; set; }

        public IEnumerable<Chart> chart { get; set; }
    }

    public class Chart
    {
        public decimal Revenu { get; set; }

        public int Month { get; set; }
    }
}