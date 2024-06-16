using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusTicketBooking.Models
{
    public class Bus
    {
        [Required]
        public string BusID { get; set; }
        [Required]
        public string BusName { get; set; }
        [Required]
        public string BusType { get; set; }
        [Required]
        public int Capacity { get; set; }
    }
}