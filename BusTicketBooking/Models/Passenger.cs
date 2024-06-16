using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusTicketBooking.Models
{
    public class Passenger
    {
        [Required(ErrorMessage ="Required")]
        public string TicketId { get; set; }
        [Required(ErrorMessage = "Required")]
        public int PassengerID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Status { get; set; }

        public virtual BTicket BTicket { get; set; }
    }
}