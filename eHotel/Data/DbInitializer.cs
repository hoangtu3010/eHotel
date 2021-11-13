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
            new User{FullName="John",Email="user2@gmail.com",Password=GetMD5("123456")},
            new User{FullName="Smith",Email="user3@gmail.com",Password=GetMD5("123456")},
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

            var statuses = new List<Status>
            {
            new Models.Status{Content="Rảnh"},
            new Models.Status{Content="Đang sử dụng"},
            };
            statuses.ForEach(s => context.Statuses.Add(s));
            context.SaveChanges();


            var rooms = new List<Room>
            {
            new Room{RoomNumber=101, Image = "~/Uploads/default.jpg", Description= "Phòng sạch sẽ!",Price= 200000, StatusId = 1, TypeId = 1 },
            new Room{RoomNumber=102, Image = "~/Uploads/default.jpg", Description= "Phòng sạch sẽ!",Price= 250000, StatusId = 2, TypeId = 2 },
            new Room{RoomNumber=103, Image = "~/Uploads/default.jpg", Description= "Phòng sạch sẽ!",Price= 500000, StatusId = 1, TypeId = 3 },
            new Room{RoomNumber=104, Image = "~/Uploads/default.jpg", Description= "Phòng sạch sẽ!",Price= 300000, StatusId = 1, TypeId = 2 },
            new Room{RoomNumber=105, Image = "~/Uploads/default.jpg", Description= "Phòng sạch sẽ!",Price= 280000, StatusId = 1, TypeId = 2 },
            new Room{RoomNumber=106, Image = "~/Uploads/default.jpg", Description= "Phòng sạch sẽ!",Price= 350000, StatusId = 2, TypeId = 1 },
            new Room{RoomNumber=107, Image = "~/Uploads/default.jpg", Description= "Phòng sạch sẽ!",Price= 150000, StatusId = 1, TypeId = 1 },
            new Room{RoomNumber=108, Image = "~/Uploads/default.jpg", Description= "Phòng sạch sẽ!",Price= 400000, StatusId = 2, TypeId = 1 },
            new Room{RoomNumber=109, Image = "~/Uploads/default.jpg", Description= "Phòng sạch sẽ!",Price= 300000, StatusId = 1, TypeId = 1 },
            new Room{RoomNumber=110, Image = "~/Uploads/default.jpg", Description= "Phòng sạch sẽ!",Price= 450000, StatusId = 1, TypeId = 3 },
           
            };
            rooms.ForEach(s => context.Rooms.Add(s));
            context.SaveChanges();
        }
    }

}