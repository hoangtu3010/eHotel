using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eHotel.Models;
using PagedList;

namespace eHotel.Areas.Admin.Controllers
{
    public class BookingsAdminController : Controller
    {
        private SystemDbContext db = new SystemDbContext();

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var bookings = db.Bookings.Include(b => b.Room).Include(b => b.User);

            ViewBag.CurrentSort = sortOrder;
            ViewBag.BookingSortParm = String.IsNullOrEmpty(sortOrder) ? "bookings_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(s => (s.Room.RoomNumber).ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "bookings_desc":
                    bookings = bookings.OrderByDescending(s => s.Room.RoomNumber);
                    break;
                default:
                    bookings = bookings.OrderBy(s => s.Room.RoomNumber);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(bookings.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Image", booking.RoomId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", booking.UserId);
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CheckIn,CheckOut,Total,Status,PaymentType,UserId,RoomId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Image", booking.RoomId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", booking.UserId);
            return View(booking);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
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
