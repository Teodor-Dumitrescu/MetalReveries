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

        public string Title
        {
            get
            {
                if (Id == 0)
                    return "New Genre";
                else
                    return "Edit Genre";
            }
        }

        public BandFormViewModel()
        {
            Id = 0;
            NrAlbumsOnSite = 0;
        }

        public BandFormViewModel(Band band)
        {
            Id = band.Id;
            Name = band.Name;
            Country = band.Country;
            YearFounded = band.YearFounded;
        }
    }
}