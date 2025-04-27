using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController:ControllerBase
    {
        [HttpGet("not found")] // GET: /api/Buggy/not found

        public IActionResult GetNotFoundRequest() { 
        // code 
        return NotFound(); //404
        }



        [HttpGet("servererror")] // GET: /api/Buggy/servererror

        public IActionResult GetServerErrorRequest()
        {
            // code 
            throw new Exception();
            return Ok();
        }

        [HttpGet("badrequest")] // GET: /api/Buggy/badrequest

        public IActionResult GetBadRequest()
        {
            // code 
            return BadRequest(); //400
        }
        [HttpGet("badrequest/{id}")] // GET: /api/Buggy/badrequest/12

        public IActionResult GetBadRequest(int id) //Validation error
        { 
            // code 
            return BadRequest(); //400
        }

        [HttpGet(template:"Unauthorized")] // GET: /api/Buggy/badrequest/Unauthorized

        public IActionResult GetUnauthorizedRequest() //Validation error
        {
            // code 
            return Unauthorized(); //401
        }
    }
}
