using MetalReveries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MetalReveries.ViewModels;

namespace MetalReveries.Controllers
{
    public class BandsController : Controller
    {
        private ApplicationDbContext _context;

        public BandsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Bands
        public ActionResult Index()
        {
            return View("AllBands");
        }

        public ActionResult New()
        {
            var viewModel = new BandFormViewModel();

            return View("BandForm", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Band band)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new BandFormViewModel(band);

                return View("BandForm", viewModel);
            }

            if (band.Id == 0)
            {
                band.NrAlbumsOnSite = 0;
                _context.Bands.Add(band);
            }
            else
            {
                var bandOld = _context.Bands.Single(m => m.Id == band.Id);
                bandOld.Name = band.Name;
                bandOld.YearFounded = band.YearFounded;
                bandOld.Country = band.Country;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Bands");
        }

        public ActionResult Edit(int id)
        {
            var bandOld = _context.Bands.SingleOrDefault(m => m.Id == id);

            if (bandOld == null)
                return HttpNotFound();

            var viewModel = new BandFormViewModel(bandOld);

            return View("BandForm", viewModel);
        }
    }
}