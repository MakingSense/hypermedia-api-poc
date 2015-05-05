using Microsoft.AspNet.Mvc;

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
