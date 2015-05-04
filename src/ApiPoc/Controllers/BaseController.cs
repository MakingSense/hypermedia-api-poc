using Microsoft.AspNet.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Linq;

namespace ApiPoc.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Negotiated<T>(T result)
        {
            // Another quick and dirty patch!
            var acceptHeader = Request.Headers["Accept"];
            if (acceptHeader != null && acceptHeader.Contains("text/html"))
            {
                return View(result);
            }
            else
            {
                return new ObjectResult(result);
            }
        }
    }
}