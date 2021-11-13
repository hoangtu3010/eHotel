using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eHotel.Models
{
    public class MultipleHome
    {
        public Booking booking { get; set; }

        public IEnumerable<Room> rooms { get; set; }

        public IEnumerable<Type> types { get; set; }
        
        public IEnumerable<Status> statuses { get; set; }
    }
}