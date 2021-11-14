using eHotel.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace eHotel.Controllers
{
    [Authorize(Roles = "Admin, Moderator, Staff")]
    public class BookingListController : Controller
    {
        private SystemDbContext db = new SystemDbContext();

        // GET: BookingList
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            var bookings = db.Bookings.Include(b => b.Room).Where(x => x.Status == (Booking.StatusOption)2);

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
                bookings = bookings.Where(s => (s.Room.RoomNumber).ToString().Contains(searchString) || s.Cmnd.Contains(searchString) || s.Tel.Contains(searchString));
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

        [HttpGet, Route("BookingList/CheckOut/{Id}")]
        public ActionResult CheckOut(int Id)
        {
            Booking booking = new Booking();

            var query = db.Bookings.Find(Id);

            query.CheckOut = DateTime.Now;

            if(query != null)
            {
                booking = query;
            }
            
            return PartialView("_CheckOutModalPartial", booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut([Bind(Include = "Id,Name,Cmnd,Tel,CheckIn,CheckOut,Total,PaymentType,RoomId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                booking.Status = (Booking.StatusOption)1;

                var room = db.Rooms.Find(booking.RoomId);
                room.StatusId = 1;

                var days = ((int)(booking.CheckOut.Value - booking.CheckIn).TotalDays + 1);

                booking.Total = days * room.Price;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Image", booking.RoomId);

            return View(booking);
        }
    }
}