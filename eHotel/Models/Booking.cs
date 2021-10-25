using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eHotel.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày đặt phòng!")]
        public DateTime CheckIn { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày trả phòng!")]
        public DateTime CheckOut { get; set; }

        public decimal Total { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
    }
}