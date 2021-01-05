using MetalReveries.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MetalReveries.ViewModels
{
    public class AlbumFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        public IEnumerable<Band> Bands { get; set; }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Band")]
        public int BandId { set; get; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { set; get; }

        [Required]
        [Range(0, 1000)]
        [Display(Name = "Number in Stock")]
        public int NumberInStock { set; get; }

        [Display(Name = "Genre")]
        [Required]
        public int GenreId { set; get; }

        [Required]
        [Display(Name="Price(RON)")]
        public float Price { set; get; }

        public string Title
        {
            get
            {
                return Id != 0 ? "Edit Movie" : "New Movie";
            }
        }

        public AlbumFormViewModel()
        {
            Id = 0;
        }

        public AlbumFormViewModel(Album album)
        {
            Id = album.Id;
            Name = album.Name;
            GenreId = album.GenreId;
            BandId = album.BandId;
            NumberInStock = album.NumberInStock;
            Price = album.Price;
            ReleaseDate = album.ReleaseDate;
        }
    }
}