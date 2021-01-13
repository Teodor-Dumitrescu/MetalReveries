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
            var user = (RegisterViewModel)validationContext.ObjectInstance;

            if (user.BirthDay == null)
                return new ValidationResult("Date of Birth is required.");

            var ageYears = DateTime.Today.Year - user.BirthDay.Year;

            var ageMonths = DateTime.Today.Month - user.BirthDay.Month;

            var ageDays = DateTime.Today.Day - user.BirthDay.Day;

            if (ageYears  >= 19 || 
                (ageYears == 18 && ageMonths >= 0) ||
                (ageYears == 18 && ageMonths == 0 && ageDays >= 0))
                return ValidationResult.Success;

            return new ValidationResult("User should be at least 18 years old");
        }
    }
}