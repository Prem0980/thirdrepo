using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusTicketBooking.Models
{
    public class BTicket
    {
        [Required(ErrorMessage = "Required")]
        public string TicketID { get; set; }
        [Required(ErrorMessage = "Required")]
        public int CustomerID { get; set; }
        public int NumberOfTickets { get; set; }
        [Required(ErrorMessage = "Required")]
        public string ScheduleId { get; set; }
        public Nullable<System.DateTime> DateOfBooking { get; set; }
        public virtual CDetails CustomerDetail { get; set; }
        public virtual ScheduleDetails Schedule { get; set; }
       
        public virtual Passenger Passengers { get; set; }
    }
}