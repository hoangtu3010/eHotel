using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eHotel.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng điền họ và tên!")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng điền email!")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Vui lòng nhập đúng định dạng email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng điền password!")]
        public string Password { get; set; }
    }
}