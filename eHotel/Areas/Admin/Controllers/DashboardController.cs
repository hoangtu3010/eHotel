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

            multipleAdmin.room = rooms;
            multipleAdmin.user = users;

            return View(multipleAdmin);
        }
    }
}