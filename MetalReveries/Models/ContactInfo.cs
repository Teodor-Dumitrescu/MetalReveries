using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MetalReveries.Models
{
    public class ContactInfo
    {
        [ForeignKey("Band")]
        public int ContactInfoId { get; set; }

        [Required]
        [RegularExpression(@"^07(\d{8})$", ErrorMessage = "This is not a valid phone number!")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Facebook { get; set; }

        public virtual Band Band { get; set; }
    }
}