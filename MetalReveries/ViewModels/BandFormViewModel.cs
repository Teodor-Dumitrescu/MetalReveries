using MetalReveries.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MetalReveries.ViewModels
{
    public class BandFormViewModel
    {
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [Required]
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

        public string Facebook { get; set; }

        public string Title
        {
            get
            {
                if (Id == 0)
                    return "New Band";
                else
                    return "Edit Band";
            }
        }

        public BandFormViewModel()
        {
            Id = 0;
            NrAlbumsOnSite = 0;
        }

        public BandFormViewModel(Band band, ContactInfo contact)
        {
            Id = band.BandId;
            Name = band.Name;
            Country = band.Country;
            YearFounded = band.YearFounded;
            PhoneNumber = contact.PhoneNumber;
            Email = contact.Email;
            Facebook = contact.Facebook;
        }
    }
}