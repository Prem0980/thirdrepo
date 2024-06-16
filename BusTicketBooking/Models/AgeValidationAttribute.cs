using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusTicketBooking.Models
{
    public class AgeValidationAttribute:ValidationAttribute
    {
        private readonly int minAge;
        private readonly int maxAge;
        public AgeValidationAttribute(int min, int max)
        {
            minAge = min;
            maxAge = max;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateOfBirth)
            {
                int age = CalculateAge(dateOfBirth);
                if (age < minAge || age > maxAge)
                {
                    return new ValidationResult($"Age must be between " + minAge + " " + "and " + maxAge + " " + "years.");
                }
            }
            return ValidationResult.Success;
        }
        private int CalculateAge(DateTime dateOfBirth)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }
    }
}