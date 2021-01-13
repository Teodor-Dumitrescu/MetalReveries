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
            // var viewModel = new BandFormViewModel();
            var viewModel = new BandContactViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult New(BandContactViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            ContactInfo contact = new ContactInfo
            {
                PhoneNumber = viewModel.PhoneNumber,
                Email = viewModel.Email,
                Facebook = viewModel.Facebook
            };

            _context.ContactInfos.Add(contact);

            Band band = new Band
            {
                Name = viewModel.Name,
                NrAlbumsOnSite = viewModel.NrAlbumsOnSite,
                Country = viewModel.Country,
                YearFounded = viewModel.YearFounded,
                ContactInfo = contact,
            };

            _context.Bands.Add(band);
            _context.SaveChanges();

            return RedirectToAction("Index", "Bands");
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(BandFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("BandForm", viewModel);
            }

            if (viewModel.Id == 0)
            {
                ContactInfo contact = new ContactInfo
                {
                    PhoneNumber = viewModel.PhoneNumber,
                    Email = viewModel.Email,
                    Facebook = viewModel.Facebook
                };

                _context.ContactInfos.Add(contact);

                Band band = new Band
                {
                    Name = viewModel.Name,
                    Country = viewModel.Country,
                    YearFounded = viewModel.YearFounded,
                    NrAlbumsOnSite = viewModel.NrAlbumsOnSite,
                    ContactInfo = contact
                };

                _context.Bands.Add(band);
            }
            else
            {
                var bandOld = _context.Bands.Single(m => m.BandId == viewModel.Id);
                bandOld.Name = viewModel.Name;
                bandOld.YearFounded = viewModel.YearFounded;
                bandOld.Country = viewModel.Country;

                var contactOld = _context.ContactInfos.Single(m => m.ContactInfoId == bandOld.ContactInfo.ContactInfoId);
                contactOld.PhoneNumber = viewModel.PhoneNumber;
                contactOld.Email = viewModel.Email;
                contactOld.Facebook = viewModel.Facebook;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Bands");
        }

        public ActionResult Edit(int id)
        {
            var bandOld = _context.Bands.SingleOrDefault(m => m.BandId == id);

            if (bandOld == null)
                return HttpNotFound();

            var contactOld = _context.ContactInfos.SingleOrDefault(m => m.ContactInfoId == bandOld.ContactInfo.ContactInfoId);

            if (contactOld == null)
                return HttpNotFound();

            var viewModel = new BandFormViewModel(bandOld, contactOld);

            return View("BandForm", viewModel);
        }
        
    }
}