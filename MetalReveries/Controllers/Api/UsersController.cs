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
    public class UsersController : ApiController
    {
        private ApplicationDbContext _context;

        public UsersController()
        {
            _context = new ApplicationDbContext();
        }

        [Route("getUsers")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult getUsers()
        {
            var users = _context.Users.Select(u => new { Id = u.Id, Email = u.Email, AlbumsBought = u.Albums.Count() });

            return Ok(users);
        }
    }
}
