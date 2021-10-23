using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eHotel.Models
{
    public class Type
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng điền tên loại!")]
        public string Name { get; set; }
    }
}