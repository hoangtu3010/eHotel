using eHotel.Areas.Admin.Controllers;
using eHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace eHotel.Data
{
    public class DbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SystemDbContext>
    {
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

        protected override void Seed(SystemDbContext context)
        {
            var Admins = new List<Admin>
            {
            new Admin{UserName="Carson",Email="Admin@gmail.com",Password=GetMD5("123456")},
            };

            Admins.ForEach(s => context.Admins.Add(s));
            context.SaveChanges();
            var Users = new List<User>
            {
            new User{FullName="Carson",Email="user1@gmail.com",Password=GetMD5("123456")},
            new User{FullName="Carson",Email="user2@gmail.com",Password=GetMD5("123456")},
            new User{FullName="Carson",Email="user3@gmail.com",Password=GetMD5("123456")},
            };

            Users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

            var types = new List<Models.Type>
            {
            new Models.Type{Name="Đơn"},
            new Models.Type{Name="Đôi"},
            new Models.Type{Name="Gia đình"},
            };
            types.ForEach(s => context.Types.Add(s));
            context.SaveChanges();
            var rooms = new List<Room>
            {
            new Room{RoomNumber=101, Image = null, Description= "Phòng sạch sẽ!",Price= 200000, Status = "Using", TypeId = 1 },
            new Room{RoomNumber=102, Image = null, Description= "Phòng sạch sẽ!",Price= 250000, Status = "Using", TypeId = 2 },
            new Room{RoomNumber=103, Image = null, Description= "Phòng sạch sẽ!",Price= 300000, Status = "Using", TypeId = 3 },
           
            };
            rooms.ForEach(s => context.Rooms.Add(s));
            context.SaveChanges();
        }
    }

}