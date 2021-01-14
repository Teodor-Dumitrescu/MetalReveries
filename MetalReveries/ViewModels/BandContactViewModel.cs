using MetalReveries.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MetalReveries.ViewModels
{
    public class BandContactViewModel
    {
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string Country { get; set; }

        [Required]
        [Range(1900, 2021)]
        public int YearFounded { get; set; }

        public int NrAlbumsOnSite { get; set; }

        [Required]
        [RegularExpression(@"^07(\d{8})$", ErrorMessage = "This is not a valid phone number!")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100)]
        public string Facebook { get; set; }

        public BandContactViewModel()
        {
            NrAlbumsOnSite = 0;
        }
    }
}