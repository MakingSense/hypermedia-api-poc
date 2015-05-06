using ApiPoc.Helpers;
using ApiPoc.PersistenceModel;
using ApiPoc.Representations;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.WebUtilities;
using System;

namespace ApiPoc.Controllers
{
    public class BaseController : Controller
    {
        public IDatabase Database { get; private set; }

        public BaseController(IDatabase database)
        {
            Database = database;
        }

        public NegotiatedResult NegotiatedResult(IRepresentation value)
        {
            return new NegotiatedResult(value);
        }

        public ErrorResult ErrorResult(ErrorRepresentation value)
        {
            return new ErrorResult(value);
        }

        public NegotiatedResult DoneResult(SimpleRepresentation value)
        {
            return new NegotiatedResult(value)
            {
                CustomHtmlView = "Done"
            };
        }

        [Obsolete("Only for demo purposes, empty results should be avoided, see http://blog.ploeh.dk/2013/04/30/rest-lesson-learned-avoid-204-responses/")]
        public NegotiatedResult DoneResult()
        {
            return new NegotiatedResult(new SimpleRepresentation())
            {
                CustomHtmlView = "Done",
                CustomStatusCode = StatusCodes.Status204NoContent
            };
        }
    }
}
