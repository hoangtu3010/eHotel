using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eHotel.Models
{
    public class SystemDbContext : DbContext
    {
        public SystemDbContext() : base("eHotel"){}

        public DbSet<Admin> Admins { get; set; } 

        public DbSet<User> Users { get; set; } 
        
        public DbSet<Type> Types { get; set; } 
        
        public DbSet<Room> Rooms { get; set; } 
        
        public DbSet<Booking> Bookings { get; set; } 
    }
}