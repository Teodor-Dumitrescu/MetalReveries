using MetalReveries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MetalReveries.Controllers.Api
{
    public class BandsController : ApiController
    {
        private ApplicationDbContext _context;

        public BandsController()
        {
            _context = new ApplicationDbContext();
        }

        //Get /api/bands
        public IHttpActionResult GetBands()
        {
            var bands = _context.Bands.ToList();

            return Ok(bands);
        }

        //Get /api/genres/1
        public IHttpActionResult GetBands(int id)
        {
            var band = _context.Bands.SingleOrDefault(m => m.Id == id);

            if (band == null)
                return NotFound();

            return Ok(band);
        }

        //Post /api/bands
        [HttpPost]
        public IHttpActionResult AddBand(Band band)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _context.Bands.Add(band);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + band.Id), band);
        }

        //PUT /api/bands/1
        [HttpPut]
        public IHttpActionResult UpdateBand(int id, Band band)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var oldBand = _context.Bands.SingleOrDefault(m => m.Id == id);

            if (oldBand == null)
                return NotFound();

            oldBand.Country = band.Country;
            oldBand.YearFounded = band.YearFounded;
            oldBand.Name = band.Name;

            _context.SaveChanges();

            return Ok();
        }

        //DELETE /api/bands/1
        public IHttpActionResult DeleteBand(int id)
        {
            var band = _context.Bands.SingleOrDefault(m => m.Id == id);

            if (band == null)
                return NotFound();

            _context.Bands.Remove(band);
            _context.SaveChanges();

            return Ok();
        }
    }
}
