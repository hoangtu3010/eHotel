using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eHotel.Models;

namespace eHotel.Controllers
{
    public class RoomsController : Controller
    {
        private SystemDbContext db = new SystemDbContext();

        // GET: Rooms
        public ActionResult Index()
        {
            var rooms = db.Rooms.Include(r => r.Type);
            return View(rooms.ToList());
        } 
       
        public ActionResult Seen()
        {
            return View();
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            SeenRoom SessionRoom = (SeenRoom)Session["SessionRoom"];
            if( SessionRoom == null)
            {
                SessionRoom = new SeenRoom();
            }
            SessionRoom.AddToSeen(room);
            Session["SessionRoom"] = SessionRoom;
          
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }
        public ActionResult Booking(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            MultiRoomBooking mymodel = new MultiRoomBooking();
            mymodel.room = room;
            return View(mymodel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
