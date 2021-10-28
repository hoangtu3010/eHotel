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
    }
}