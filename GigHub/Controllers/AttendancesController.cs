﻿using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend([FromBody] int gigId)
        {
            var userId = User.Identity.GetUserId();
            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == gigId))
                return BadRequest("Attendance already exists.");

            var attendace = new Attendance
            {
                GigId = gigId,
                AttendeeId = User.Identity.GetUserId()
            };

            _context.Attendances.Add(attendace);
            _context.SaveChanges();

            return Ok();
        }
    }
}
