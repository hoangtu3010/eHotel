using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eHotel.Models;

namespace eHotel.Areas.Admin.Controllers
{
    public class UserRoleMappingsController : Controller
    {
        private SystemDbContext db = new SystemDbContext();

        public ActionResult Index()
        {
            var userRoleMappings = db.UserRoleMappings.Include(u => u.Admin).Include(u => u.Roles);
            return View(userRoleMappings.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRoleMapping userRoleMapping = db.UserRoleMappings.Find(id);
            if (userRoleMapping == null)
            {
                return HttpNotFound();
            }
            return View(userRoleMapping);
        }

        public ActionResult Create()
        {
            ViewBag.AdminId = new SelectList(db.Admins, "Id", "UserName");
            ViewBag.RolesId = new SelectList(db.Roles, "Id", "RoleName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AdminId,RolesId")] UserRoleMapping userRoleMapping)
        {
            if (ModelState.IsValid)
            {
                db.UserRoleMappings.Add(userRoleMapping);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdminId = new SelectList(db.Admins, "Id", "UserName", userRoleMapping.AdminId);
            ViewBag.RolesId = new SelectList(db.Roles, "Id", "RoleName", userRoleMapping.RolesId);
            return View(userRoleMapping);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRoleMapping userRoleMapping = db.UserRoleMappings.Find(id);
            if (userRoleMapping == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdminId = new SelectList(db.Admins, "Id", "UserName", userRoleMapping.AdminId);
            ViewBag.RolesId = new SelectList(db.Roles, "Id", "RoleName", userRoleMapping.RolesId);
            return View(userRoleMapping);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AdminId,RolesId")] UserRoleMapping userRoleMapping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userRoleMapping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdminId = new SelectList(db.Admins, "Id", "UserName", userRoleMapping.AdminId);
            ViewBag.RolesId = new SelectList(db.Roles, "Id", "RoleName", userRoleMapping.RolesId);
            return View(userRoleMapping);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRoleMapping userRoleMapping = db.UserRoleMappings.Find(id);
            if (userRoleMapping == null)
            {
                return HttpNotFound();
            }
            return View(userRoleMapping);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserRoleMapping userRoleMapping = db.UserRoleMappings.Find(id);
            db.UserRoleMappings.Remove(userRoleMapping);
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
