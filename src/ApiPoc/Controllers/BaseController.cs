using ApiPoc.PersistenceModel;
using ApiPoc.Representations;
using Microsoft.AspNet.Mvc;


namespace ApiPoc.Controllers
{
    public class BaseController : Controller
    {
        public IDatabase Database { get; private set; }

        public BaseController(IDatabase database)
        {
            Database = database;
        }

        public IActionResult Negotiated<T>(T value)
        {
            // Another quick and dirty patch!
            var error = value as ErrorRepresentation;
            var acceptHeader = Request.Headers["Accept"];
            if (acceptHeader != null && acceptHeader.Contains("text/html"))
            {
                ViewResult result;
                if (error != null)
                {
                    result = View("Error", value);
                    result.StatusCode = error.Code;
                }
                else
                {
                    result = View(value);
                }
                return result;
            }
            else
            {
                var result = new ObjectResult(value);
                if (error != null)
                {
                    result.StatusCode = error.Code;
                }
                return result;
            }
        }
    }
}
