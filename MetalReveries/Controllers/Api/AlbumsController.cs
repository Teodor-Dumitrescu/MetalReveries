using MetalReveries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MetalReveries.Controllers.Api
{
    public class AlbumsController : ApiController
    {
        private ApplicationDbContext _context;

        public AlbumsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/albums
        public IHttpActionResult GetAlbums()
        {
            var albums = _context.Albums
                .Include(m => m.Genre)
                .Include(m => m.Band)
                .ToList();

            return Ok(albums);
        }

        // Get /api/albums/get2
        [HttpGet]
        [Route("api/albums/get2")]
        public IHttpActionResult GetAlbums2()
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            var albums = currentUser.Albums;

            foreach (Album album in albums)
            {
                var genre = _context.Genres.SingleOrDefault(m => m.Id == album.GenreId);
                var band = _context.Bands.SingleOrDefault(m => m.BandId == album.BandId);

                if (genre != null)
                    album.Genre = genre;

                if (band != null)
                    album.Band = band;
            }

            return Ok(albums);
        }

        // Get /api/albums/get2
        [HttpGet]
        [Route("api/albums/get3/{id}")]
        public IHttpActionResult GetAlbums3(string id)
        {
            //return BadRequest();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            ApplicationUser currentUser = UserManager.FindById(id);

            if (currentUser == null)
                return BadRequest();

            var albums = currentUser.Albums;

            foreach (Album album in albums)
            {
                var genre = _context.Genres.SingleOrDefault(m => m.Id == album.GenreId);
                var band = _context.Bands.SingleOrDefault(m => m.BandId == album.BandId);

                if (genre != null)
                    album.Genre = genre;

                if (band != null)
                    album.Band = band;
            }

            return Ok(albums);
        }

        // GET /api/albums/1
        public IHttpActionResult GetAlbum(int id)
        {
            var album = _context.Albums.SingleOrDefault(m => m.Id == id);

            if (album == null)
                return NotFound();

            return Ok(album);
        }

        // POST /api/albums
        [HttpPost]
        public IHttpActionResult AddAlbum(Album album)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _context.Albums.Add(album);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + album.Id), album);
        }

        // PUT /api/albums/1
        [HttpPut]
        public IHttpActionResult UpdateAlbum(int id, Album album)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var albumOld = _context.Albums.SingleOrDefault(m => m.Id == id);

            if (albumOld == null)
                return NotFound();

            //To DO:

            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/albums/1
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteAlbum(int id)
        {
            var albumOld = _context.Albums.SingleOrDefault(m => m.Id == id);

            if (albumOld == null)
                return NotFound();

            var oldBand = _context.Bands.SingleOrDefault(m => m.BandId == albumOld.BandId);
            if (oldBand != null)
                oldBand.NrAlbumsOnSite -= 1;

            var oldGenre = _context.Genres.SingleOrDefault(m => m.Id == albumOld.GenreId);
            if (oldGenre != null)
                oldGenre.AlbumCount -= 1;

            foreach(var user in albumOld.Users.ToList())
            {
                user.Albums.Remove(albumOld);
            }

            _context.Albums.Remove(albumOld);
            _context.SaveChanges();

            return Ok();
        }
    }
}
