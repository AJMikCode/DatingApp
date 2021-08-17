using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class BuggyController : BasicApiController
    {
        private readonly DataContext _context;
        public BuggyController(DataContext context)
        {
            _context = context;

        }
        
        // Tests 401 Unauthorized Responses 
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "Secret Text";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = _context.Users.Find(-1);
                    // Returns Not found error, most likely result unless a user has a negative id number
            if(thing == null) return NotFound();

                // returns 200 response which is ok but this is only if thing is possibel to be found which it isn't
            return Ok(thing);
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            // try {
            // Will result in null value, no user exists with id of -1 
            var thing = _context.Users.Find(-1);

            // Cannot convert thing to string since its null, null exception error
            var thingToReturn = thing.ToString();

            // Returns null exception error
            return thingToReturn;
            // }
            // catch (Exception ex) 
            // {
            //     Console.WriteLine("First exception caught", ex);
            //     return StatusCode(500,"bad msg");
            // }
        }


        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            // Really simple byt just returning a bad request with a msg inside it.
            return BadRequest("This is a bad request msg.");
        }
    }
}