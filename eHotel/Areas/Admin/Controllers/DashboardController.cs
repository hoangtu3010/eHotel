using eHotel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eHotel.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private SystemDbContext db = new SystemDbContext();

        [Authorize]
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            MultipleAdmin multipleAdmin = new MultipleAdmin();
            var rooms = db.Rooms.Include(r => r.Status).Include(r => r.Type);
            var users = db.Users.ToList();

          

            var money = db.Bookings
               .Where((c) => c.CheckOut.Year == DateTime.Now.Year).Where((c) => c.CheckOut.Month == DateTime.Now.Month).ToList();

            decimal total = 0;

            foreach (var item in money)
            {
                if (money != null)
                    total += item.Total;
            }

            ViewBag.Total = total;

            multipleAdmin.room = rooms;
            multipleAdmin.user = users;
            multipleAdmin.booking = db.Bookings.ToList();

            return View(multipleAdmin);
        }

        public ActionResult GetData()
        {
            var chart = db.Bookings
               .Where((c) => c.CheckOut.Year == DateTime.Now.Year)
               .GroupBy((c) => c.CheckOut.Month)
               .Select((c) => new Chart { Revenu = c.Sum(p => p.Total), Month = c.Key })
               .ToList();

            return Json(chart, JsonRequestBehavior.AllowGet);
        }
    }
}