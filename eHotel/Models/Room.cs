using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eHotel.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng điền số phòng!")]
        public int RoomNumber { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }
        
        [Required(ErrorMessage = "Vui lòng điền giá!")]
        [DisplayFormat(DataFormatString = "{0:#}")]
        public decimal Price { get; set; }

        public int StatusId { get; set; }

        public virtual Status Status { get; set; }

        public int TypeId { get; set; }

        public virtual Type Type { get; set; }
    }
}