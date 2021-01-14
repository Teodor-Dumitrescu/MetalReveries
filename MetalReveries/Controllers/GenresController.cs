using MetalReveries.Models;
using MetalReveries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetalReveries.Controllers
{
    [Authorize]
    public class GenresController : Controller
    {

        private ApplicationDbContext _context;

        public GenresController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Genres
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
                return View("AllGenres");

            return View("AllGenresNoEdit");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult New()
        {
            var viewModel = new GenreFormViewModel();

            return View("GenreForm", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Save(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new GenreFormViewModel(genre);

                return View("GenreForm", viewModel);
            }
            
            if (genre.Id == 0)
            {
                genre.AlbumCount = 0;

                _context.Genres.Add(genre);
            }
            else
            {
                var genreOld = _context.Genres.Single(m => m.Id == genre.Id);
                genreOld.Id = genre.Id;
                genreOld.Name = genre.Name;
                // genreOld.AlbumCount = genre.AlbumCount;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Genres");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var genre = _context.Genres.SingleOrDefault(m => m.Id == id);

            if (genre == null)
                return HttpNotFound();

            var viewModel = new GenreFormViewModel(genre);

            return View("GenreForm", viewModel);
        }
    }
}