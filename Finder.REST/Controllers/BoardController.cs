using Finder.Business.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Finder.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public BoardController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        // POST api/<BoardController>
        [HttpPost("init")]
        public IActionResult Post([FromBody] string[] value)
        {
            var filename = string.Empty;
            if (value.Length == 0)
                return BadRequest();
            try
            {
                filename = Guid.NewGuid().ToString();
                var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, filename + ".txt");

                System.IO.File.WriteAllText(filePath, string.Join(Environment.NewLine, value));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok(filename);
        }

        // POST api/<BoardController>
        [HttpPost("find/{uid}")]
        public IActionResult Find([FromBody] string[] value, string uid)
        {
            var filename = string.Empty;
            IEnumerable<string> retval = new List<string>();
            if (value.Length == 0 || string.IsNullOrEmpty(uid))
                return BadRequest();
            try
            {
                var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, uid + ".txt");

                if (System.IO.File.Exists(filePath))
                {
                    var list = System.IO.File.ReadAllLines(filePath);
                    var finder = new WordFinder(list);
                    retval = finder.Find(value);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NoContent, "The give uid doesn't have a valid board.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok(retval);
        }
    }
}
