using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eHotel.Models;
using PagedList;

namespace eHotel.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Moderator")]
    public class RoomsAdminController : Controller
    {
        private SystemDbContext db = new SystemDbContext();

        // GET: Admin/Rooms
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var rooms = db.Rooms.Include(r => r.Status).Include(r => r.Type);

            ViewBag.CurrentSort = sortOrder;
            ViewBag.RoomSortParm = String.IsNullOrEmpty(sortOrder) ? "room_desc" : "";

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
                rooms = rooms.Where(s => (s.RoomNumber).ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "room_desc":
                    rooms = rooms.OrderByDescending(s => s.RoomNumber);
                    break;
                default:
                    rooms = rooms.OrderBy(s => s.RoomNumber);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(rooms.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Rooms/Details/5
        public ActionResult Details(int? id)
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
            return View(room);
        }

        // GET: Admin/Rooms/Create
        public ActionResult Create()
        {
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Content");
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name");
            return View();
        }

        // POST: Admin/Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RoomNumber,Description,Price,StatusId,TypeId")] Room room, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                string roomImage = "~/Uploads/default.jpg";
                try
                {
                    if(Image != null)
                    {
                        string fileName = Path.GetFileName(Image.FileName);
                        string path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                        Image.SaveAs(path);
                        roomImage = "~/Uploads/" + fileName;
                    }
                }
                catch { }
                finally
                {
                    room.Image = roomImage;
                }

                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Content", room.StatusId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", room.TypeId);
            return View(room);
        }

        // GET: Admin/Rooms/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Content", room.StatusId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", room.TypeId);
            return View(room);
        }

        // POST: Admin/Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RoomNumber,Image,Description,Price,StatusId,TypeId")] Room room, HttpPostedFileBase InputImage)
        {
            if (ModelState.IsValid)
            {
                string roomImage = room.Image;

                try
                {
                    if (InputImage != null)
                    {
                        string fileName = Path.GetFileName(InputImage.FileName);
                        string path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                        InputImage.SaveAs(path);
                        roomImage = "~/Uploads/" + fileName;
                    }
                }
                catch { }
                finally
                {
                    room.Image = roomImage;
                }

                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Content", room.StatusId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", room.TypeId);
            return View(room);
        }

        // GET: Admin/Rooms/Delete/5
        public ActionResult Delete(int? id)
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
            return View(room);
        }

        // POST: Admin/Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
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
