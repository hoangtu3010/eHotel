using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eHotel.Models
{
    public class SeenRoom
    {
        private List<Room> Seens;
        public SeenRoom()
        {
            Seens = new List<Room>();
        }
        public List<Room> SeenRooms { get => Seens; }
        public Room this [ int index]
        {
            get => SeenRooms[index];
            set => SeenRooms[index]= value;
        }
        public void AddToSeen (Room room)
        {
            if (!CheckExist(room))
            {
                SeenRooms.Add(room);
            }
        }
        public bool CheckExist(Room room)
        {
            foreach(Room r in SeenRooms)
            {
                if(r.Id == room.Id)
                {
                    return true;
                }
            }
            return false;
        }

    }
}