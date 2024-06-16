using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusTicketBooking.Models
{
    public class CDetails
    {
        [Required]
        public long CustomerID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public int Pincode { get; set; }
        [Required]
        public string EmailID { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public long ContactNumber { get; set; }
        [Required]
        public System.DateTime DateOfBirth { get; set; }
        [Required]
       // [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Please Enter a valid Password with at least 8 characters including at least one alphabet, one number, and one special character.")]
        public string Customerpassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        public virtual State State2 { get; set; }
    }
}