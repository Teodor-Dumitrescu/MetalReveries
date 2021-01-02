using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MetalReveries.Models
{
    public class AgeRestriction18 : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.Birthdate == null)
                return new ValidationResult("Birthdate is required.");

            var ageYears = DateTime.Today.Year - customer.Birthdate.Value.Year;

            var ageMonths = DateTime.Today.Month - customer.Birthdate.Value.Month;

            var ageDays = DateTime.Today.Day - customer.Birthdate.Value.Day;

            if (ageYears  >= 19 || 
                (ageYears == 18 && ageMonths >= 0) ||
                (ageYears == 18 && ageMonths == 0 && ageDays >= 0))
                return ValidationResult.Success;

            return new ValidationResult("Customer should be at least 18 years old");
        }
    }
}