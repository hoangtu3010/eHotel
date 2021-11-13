using eHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace eHotel.Controllers
{
    public class HomeController : Controller
    {
        private SystemDbContext db = new SystemDbContext();

        [Authorize]
        public ActionResult Index()
        {

            MultipleHome multipleHome = new MultipleHome();

            var rooms = db.Rooms.Include(r => r.Status).Include(r => r.Type);
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
            try
            {
                if (ModelState.IsValid)
                {
                    string currentName = User.Identity.Name;
                    var userId = db.Users.Where(x => x.Email == currentName).Select(x => x.Id).FirstOrDefault();
                    var room = db.Rooms.Find(multi.booking.RoomId);
                    var days = ((int)(multi.booking.CheckOut - multi.booking.CheckIn).TotalDays);

                    multi.booking.Total = days * room.Price;

                    db.Bookings.Add(multi.booking);
                    db.SaveChanges();
                    return Redirect("~/Rooms");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Redirect("~/Rooms");
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