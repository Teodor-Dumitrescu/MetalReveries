﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MetalReveries.Models
{
    public class Album
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public int BandId { set; get; }

        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { set; get; }

        [Range(0, 1000)]
        [Display(Name = "Number in Stock")]
        public byte NumberInStock { set; get; }

        [Display(Name = "Genre")]
        [Required]
        public int GenreId { set; get; }

        public Genre Genre { set; get; }

    }
}