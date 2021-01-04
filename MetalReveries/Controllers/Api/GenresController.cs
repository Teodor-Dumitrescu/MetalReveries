using MetalReveries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using AutoMapper;


namespace MetalReveries.Controllers.Api
{
    public class GenresController : ApiController
    {
        private ApplicationDbContext _context;

        public GenresController()
        {
            _context = new ApplicationDbContext();
        }

        //Get /api/genres
        public IHttpActionResult GetGenres()
        {
            var genres = _context.Genres.ToList();

            return Ok(genres);
        }

        //Get /api/genres/1
        public IHttpActionResult GetGenre(int id)
        {
            var genre = _context.Genres.SingleOrDefault(g => g.Id == id);

            if (genre == null)
                return NotFound();

            return Ok(genre);
        }

        //Post /api/genres
        [HttpPost]
        public IHttpActionResult AddGenre(Genre genre)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _context.Genres.Add(genre);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + genre.Id), genre);
        }

        //PUT /api/genres/1
        [HttpPut]
        public IHttpActionResult UpdateGenre(int id, Genre genre)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var oldGenre = _context.Genres.SingleOrDefault(g => g.Id == id);

            if (oldGenre == null)
                return NotFound();

            // oldGenre.Id = genre.Id;
            oldGenre.Name = genre.Name;
            // oldGenre.AlbumCount = genre.AlbumCount;

            _context.SaveChanges();

            return Ok();
        }

        //DELETE /api/customers/1
        public IHttpActionResult DeleteGenre(int id)
        {
            var genre = _context.Genres.SingleOrDefault(g => g.Id == id);

            if (genre == null)
                return NotFound();

            _context.Genres.Remove(genre);
            _context.SaveChanges();

            return Ok();
        }
    }
}
