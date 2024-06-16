using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTicketBooking.Models
{
    public class State
    {
        public string District { get; set; }
        public string State1 { get; set; }
        public virtual CDetails CustomerDetails { get; set; }
    }
}