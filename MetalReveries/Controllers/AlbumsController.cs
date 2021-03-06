﻿using MetalReveries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MetalReveries.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MetalReveries.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User?.Identity.IsAuthenticated == false)
                return View("AllAlbumsNotAuth");
            else
            {
                if (User.IsInRole("Admin"))
                    return View("AllAlbums");

                return View("AllAlbumsUser");
            }
        }

        public ActionResult AlbumsOwned()
        {
            return View("AlbumsOwned");
        }

        [Authorize(Roles ="Admin")]
        public ActionResult AlbumsOwnedOtherUser(string id)
        {
            ViewBag.UserId = id;
            return View("AlbumsOwnedOtherUser");
        }

        public ActionResult Buy(int id)
        {
            var album = _context.Albums.SingleOrDefault(m => m.Id == id);

            if (album == null)
                return HttpNotFound();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            var albumOld = currentUser.Albums.SingleOrDefault(m => m.Id == id);

            if (albumOld != null)
            {
                return View("AlreadyBought");
            }

            if (album.NumberInStock == 0)
                return View("OutOfStock");

            currentUser.Albums.Add(album);

            _context.SaveChanges();

            return View("AllAlbumsUser");
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

        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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