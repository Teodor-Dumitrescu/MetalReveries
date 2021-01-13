using MetalReveries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MetalReveries.ViewModels;

namespace MetalReveries.Controllers
{
    public class AlbumsController : Controller
    {
        
        private ApplicationDbContext _context;

        public AlbumsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        
        // GET: Albums
        public ActionResult Index()
        {

            return View("AllAlbums");
        }

        public ActionResult Details(int id)
        {
            var album = _context.Albums
                .Include(m => m.Genre)
                .Include(m => m.Band)
                .SingleOrDefault(m => m.Id == id);

            if (album == null)
                return HttpNotFound();

            return View(album);
        }

        public ActionResult New()
        {
            var genres = _context.Genres.ToList();

            var bands = _context.Bands.ToList();

            var viewModel = new AlbumFormViewModel
            {
                Genres = genres,
                Bands = bands
            };

            return View("AlbumForm", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Album album)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new AlbumFormViewModel(album)
                {
                    Genres = _context.Genres.ToList(),
                    Bands = _context.Bands.ToList()
                };

                return View("AlbumForm", viewModel);
            }

            if (album.Id == 0)
            {
                var bandInDb = _context.Bands.SingleOrDefault(m => m.BandId == album.BandId);
                
                if (bandInDb != null)
                    bandInDb.NrAlbumsOnSite += 1;

                var genreInDb = _context.Genres.SingleOrDefault(m => m.Id == album.GenreId);

                if (genreInDb != null)
                    genreInDb.AlbumCount += 1;

                _context.Albums.Add(album);
            }
            else
            {
                var albumOld = _context.Albums.Single(m => m.Id == album.Id);
                
                if (albumOld.BandId != album.BandId)
                {
                    var oldBand = _context.Bands.Single(m => m.BandId == albumOld.BandId);

                    oldBand.NrAlbumsOnSite -= 1;

                    var newBand = _context.Bands.SingleOrDefault(m => m.BandId == album.BandId);

                    if (newBand != null)
                        newBand.NrAlbumsOnSite += 1;
                }

                if (albumOld.GenreId != album.GenreId)
                {
                    var oldGenre = _context.Genres.Single(m => m.Id == albumOld.Id);

                    oldGenre.AlbumCount -= 1;

                    var newGenre = _context.Genres.SingleOrDefault(m => m.Id == album.Id);

                    if (newGenre != null)
                        newGenre.AlbumCount += 1;
                }

                albumOld.Name = album.Name;
                albumOld.Price = album.Price;
                albumOld.ReleaseDate = album.ReleaseDate;
                albumOld.BandId = album.BandId;
                albumOld.GenreId = album.GenreId;
                albumOld.NumberInStock = album.NumberInStock;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Albums");
        }

        public ActionResult Edit(int id)
        {
            var album = _context.Albums.SingleOrDefault(m => m.Id == id);

            if (album == null)
                return HttpNotFound();

            var viewModel = new AlbumFormViewModel(album)
            {
                Genres = _context.Genres.ToList(),
                Bands = _context.Bands.ToList()
            };

            return View("AlbumForm", viewModel);
        }
        
    }
}