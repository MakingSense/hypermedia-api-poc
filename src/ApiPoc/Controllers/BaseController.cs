using ApiPoc.Helpers;
using ApiPoc.PersistenceModel;
using ApiPoc.Representations;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.WebUtilities;
using Microsoft.Framework.OptionsModel;
using System;

namespace ApiPoc.Controllers
{
    public class BaseController : Controller
    {
        public IDatabase Database { get; private set; }
        public IOptions<AppSettings> Settings { get; private set; }

        public BaseController(IDatabase database, IOptions<AppSettings> settings)
        {
            Database = database;
            Settings = settings;
        }

        public NegotiatedResult NegotiatedResult(IRepresentation value)
        {
            return new NegotiatedResult(value);
        }

        public ErrorResult ErrorResult(ErrorRepresentation value)
        {
            return new ErrorResult(value);
        }

        public OperationResult DoneResult(OkRepresentation value)
        {
            return new OperationResult(value);
        }

        public OperationResult NotModifiedResult(OkRepresentation value)
        {
            return new OperationResult(value)
            {
                CustomStatusCode = StatusCodes.Status304NotModified
            };
        }

        [Obsolete("Only for demo purposes, empty results should be avoided, see http://blog.ploeh.dk/2013/04/30/rest-lesson-learned-avoid-204-responses/")]
        public OperationResult DoneResult()
        {
            return new OperationResult();
        }
    }
}
