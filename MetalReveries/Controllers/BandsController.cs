using MetalReveries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }

        public 
    }
}