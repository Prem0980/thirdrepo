using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace BusTicketBooking.Models
{
    public class ScheduleDetails
    {
        public string ScheduleID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string RouteID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string BusID { get; set; }
        [Required(ErrorMessage = "Required")]
        [Range(1, 30, ErrorMessage = "Please enter a value between 1 and 30")]
        public int AvailableSeats { get; set; }
        [Required(ErrorMessage = "Required")]
        public System.TimeSpan Time { get; set; }
        [Required(ErrorMessage = "Required")]
        public System.DateTime DateOfJourney { get; set; }
        [Required(ErrorMessage = "Required")]
        public int Cost { get; set; }
        public virtual ICollection<BTicket> BookTickets { get; set; }
        public virtual Bus Bus { get; set; }
        public virtual RouteDetails Route { get; set; }
    }
}