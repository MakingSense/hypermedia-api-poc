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
    }
}
