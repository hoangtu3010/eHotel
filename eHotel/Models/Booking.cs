using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eHotel.Models
{
    public class Booking
    {
        public enum StatusOption
        {
            Wait = 1,
            Accept = 2
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày đặt phòng!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CheckIn { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày trả phòng!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CheckOut { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}")]

        public decimal Total { get; set; }

        public StatusOption Status { get; set; }

        public string PaymentType { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int RoomId { get; set; }

        public virtual Room Room { get; set; }
    }

}