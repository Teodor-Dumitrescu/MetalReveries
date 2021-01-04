using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MetalReveries.Models
{
    public class Band
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        [Range(1900, 2021)]
        public int YearFounded { get; set; }

        public int NrAlbumsOnSite { get; set; }
    }
}