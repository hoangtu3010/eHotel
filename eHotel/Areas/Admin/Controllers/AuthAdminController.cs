using eHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace eHotel.Areas.Admin.Controllers
{
    public class AuthAdminController : Controller
    {
        private SystemDbContext db = new SystemDbContext();
        // GET: User
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Models.Admin admin)
        {
            if (ModelState.IsValid)
            {
                var check = db.Admins.FirstOrDefault(s => s.Email == admin.Email);
                if (check == null)
                {
                    // tao password - SHA256
                    admin.Password = GetMD5(admin.Password);
                    db.Admins.Add(admin);
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(admin.Email, true);
                    return Redirect("Home");
                }
                else
                {
                    ViewBag.Error = "UserName này đã tồn tại!";
                }
            }
            return View();
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] frData = Encoding.UTF8.GetBytes(str);
            byte[] toData = md5.ComputeHash(frData);
            string hashString = "";
            for (int i = 0; i < toData.Length; i++)
            {
                hashString += toData[i].ToString("x2");
            }
            return hashString;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                login.Password = GetMD5(login.Password);
                var data = db.Admins.Where(s => s.Email.Equals(login.Email) && s.Password.Equals(login.Password)).FirstOrDefault();
                if (data != null)
                {
                    FormsAuthentication.SetAuthCookie(data.Email, true);
                    return Redirect("Home");
                }
            }
            return View();
        }
    }

}