using eHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eHotel.Controllers
{
    public class HomeController : Controller
    {
        private SystemDbContext db = new SystemDbContext();

        public ActionResult Index()
        {
            db.Types.Add(new Models.Type() { Name = "Đơn" });
            return View();
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