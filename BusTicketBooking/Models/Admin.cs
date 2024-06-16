using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusTicketBooking.Models
{
    public class Admin
    {
        [Required(ErrorMessage ="Required")]
        public string UserID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string UserPassword { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Role { get; set; }
    }
}