using System;
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

        public Band Band { set; get; }

        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ReleaseDate { set; get; }

        [Range(0, 1000)]
        [Display(Name = "Number in Stock")]
        public int NumberInStock { set; get; }

        [Display(Name = "Genre")]
        [Required]
        public int GenreId { set; get; }

        public Genre Genre { set; get; }

        public float Price { set; get; }

        // many to many
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}