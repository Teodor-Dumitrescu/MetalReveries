using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MetalReveries.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        public string Email { get; set; }

        [Display(Name = "Date of Birth")]
        [AgeRestriction18]
        public DateTime? Birthdate { get; set; }

        public DateTime RegisterDate { get; set; }
    }
}