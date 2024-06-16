using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusTicketBooking.Models
{
    public class RouteDetails
    {
        [Required(ErrorMessage = "Required")]
        public string RouteID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string FromLocation { get; set; }
        [Required(ErrorMessage = "Required")]
        public string ToLocation { get; set; }
        public virtual ICollection<ScheduleDetails> Schedules { get; set; }
    }
}