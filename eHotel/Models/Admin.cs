using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eHotel.Models
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng điền tên tài khoản!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng điền email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng điền password!")]
        public string Password { get; set; }
    }
}