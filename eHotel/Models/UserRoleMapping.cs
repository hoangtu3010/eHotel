using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eHotel.Models
{
    public class UserRoleMapping
    {
        [Key]
        public int Id { get; set; }

        public int AdminId { get; set; }

        public virtual Admin Admin { get; set; }

        public int RolesId { get; set; }

        public virtual Roles Roles { get; set; }
    }
}