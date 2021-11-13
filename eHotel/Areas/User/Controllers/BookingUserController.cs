using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eHotel.Models;
using NLog.Fluent;
using NLog;

namespace eHotel.Areas.User.Controllers
{   [Authorize]
    public class BookingUserController : Controller
    {
        private SystemDbContext db = new SystemDbContext();


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
            mymodel.booking = new Booking();
            mymodel.booking.CheckIn = DateTime.Now;
            mymodel.booking.CheckOut = DateTime.Now.AddDays(1);

            mymodel.booking.Room = room;

            return View(mymodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Booking([Bind(Include ="booking,room")] MultiRoomBooking multi)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string currentName = User.Identity.Name;
                    var userId = db.Users.Where(x => x.Email == currentName).Select(x => x.Id).FirstOrDefault();
                    var room = db.Rooms.Find(multi.booking.RoomId);
                    var days = ((int)(multi.booking.CheckOut - multi.booking.CheckIn).TotalDays) ;

                    multi.booking.Total = days*room.Price;

                    db.Bookings.Add(multi.booking);
                    db.SaveChanges();
                    return Redirect("~/Rooms");
                }
            }catch( Exception e)
            {
                Console.WriteLine(e);
            }
            

            //ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Image", booking.RoomId);
            //ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", booking.UserId);
            return Redirect("~/Rooms");
        }

        // GET: User/BookingUser/Edit/5
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
            return View(booking);
        }

        // POST: User/BookingUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
            return View(booking);
        }

        // GET: User/BookingUser/Delete/5
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

        // POST: User/BookingUser/Delete/5
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
