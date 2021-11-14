using eHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace eHotel.Controllers
{
    [Authorize(Roles = "Admin, Moderator, Staff")]
    public class HomeController : Controller
    {
        private SystemDbContext db = new SystemDbContext();

        public ActionResult Index()
        {

            MultipleHome multipleHome = new MultipleHome();

            var rooms = db.Rooms.Include(r => r.Status).Include(r => r.Type).Where(x => x.StatusId == 1);
            var type = db.Types.ToList();
            var status = db.Statuses.ToList();

            multipleHome.booking = new Booking();
            multipleHome.booking.CheckIn = DateTime.Now;
            multipleHome.booking.CheckOut = DateTime.Now.AddDays(1);
            multipleHome.rooms = rooms;
            multipleHome.types = type;
            multipleHome.statuses = status;

            return View(multipleHome);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Booking([Bind(Include = "booking,room")] MultiRoomBooking multi)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var room = db.Rooms.Find(multi.booking.RoomId);
                    room.StatusId = 2;
                    multi.booking.Status = (Booking.StatusOption)2;
                    db.Bookings.Add(multi.booking);
                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return RedirectToAction("Index", "BookingList");
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}