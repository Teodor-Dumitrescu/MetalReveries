using MetalReveries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MetalReveries.ViewModels;

namespace MetalReveries.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        public ActionResult Index()
        {
            if(User.IsInRole("Admin"))
                return View("AllBands");

            return View("AllBandsNoEdit");
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var band = _context.Bands.SingleOrDefault(m => m.BandId == id);

            if (band == null)
                return HttpNotFound();

            return View(band);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult New()
        {
            // var viewModel = new BandFormViewModel();
            var viewModel = new BandContactViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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