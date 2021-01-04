using MetalReveries.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MetalReveries.ViewModels
{
    public class GenreFormViewModel
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Number of albums")]
        public int? AlbumCount { get; set; }

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

        public GenreFormViewModel()
        {
            Id = 0;
            AlbumCount = 0;
        }

        public GenreFormViewModel(Genre genre)
        {
            Id = genre.Id;
            Name = genre.Name;
            AlbumCount = genre.AlbumCount;
        }
    }
}