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
            Yes = 1,
            No = 2
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Cmnd { get; set; }

        public string Tel { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày đặt phòng!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CheckIn { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CheckOut { get; set; }

        [DisplayFormat(DataFormatString = "{0:###,### VND}", ApplyFormatInEditMode = true)]
        public decimal? Total { get; set; }

        public StatusOption Status { get; set; }

        [Required]
        public int RoomId { get; set; }

        public virtual Room Room { get; set; }
    }

}